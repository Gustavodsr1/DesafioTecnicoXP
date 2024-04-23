using GestaoPortfolio.Domain.Interfaces;
using GestaoPortfolio.Domain.Interfaces.Facades;
using GestaoPortfolio.Domain.Interfaces.Services;
using GestaoPortfolio.Domain.Models;
using static GestaoPortfolio.Domain.Models.Enum.EventoEnum;

namespace GestaoPortfolio.Application.Services
{
    public class AtualizarCarteiraEstoqueService : IAtualizarCarteiraEstoqueService
    {
        private readonly IOfertaFacade ofertaFacade;
        private readonly IOperacaoFacade operacaoFacade;
        private readonly IInvestimentosFacade carteiraFacade;
        private readonly IClienteRepository clienteRepository;
        private readonly IOfertaRepository ofertaRepository;
        private readonly IInvestimentosRepository carteiraRepository;


        public AtualizarCarteiraEstoqueService(IOperacaoFacade operacaoFacade, IOfertaFacade ofertaFacade, IInvestimentosFacade carteiraFacade, IInvestimentosRepository carteiraRepository, IOfertaRepository ofertaRepository, IClienteRepository clienteRepository)
        {
            this.ofertaFacade = ofertaFacade;
            this.carteiraFacade = carteiraFacade;
            this.operacaoFacade = operacaoFacade;
            this.clienteRepository = clienteRepository;
            this.carteiraRepository = carteiraRepository;  
            this.ofertaRepository = ofertaRepository;
        }

        public async Task<Operacao> TratarOperacao(Operacao operacao)
        {
            Operacao resultado = null ;
            Oferta oferta = ofertaRepository.GetById(operacao.CodigoOferta);
            Cliente cliente = clienteRepository.GetById(operacao.IdCliente);
            operacao.ValorPrecoUnitario = oferta.PrecoUnitario;
            operacao.ValorTotalOperacao = operacao.ValorPrecoUnitario * operacao.QuantidadeOperacao;
            operacao.QuantidadeDisponivelEstoque = oferta.QuantidadeDisponivel;
            operacao.CodigoProduto = oferta.CodigoProduto;

            if (operacao.Evento == Evento.Compra)
            {
                if (operacao.QuantidadeOperacao > oferta.QuantidadeDisponivel || operacao.QuantidadeOperacao <= 0)
                {
                    return resultado;
                }
                else
                {

                    await AtualizarCarteiraEstoqueCompra(oferta, operacao, cliente);
                }
            }
            else
            {
                Investimentos carteira = carteiraRepository.BuscarCarteiraCliente(operacao.CodigoOferta, operacao.IdCliente);

                //Se a solicitação de venda for maior que a quantidade da posição, entende-se que é um resgate completo e ajusta-se a quantidade para a mesma da carteira
                if (operacao.QuantidadeOperacao > carteira.Quantidade)
                {
                    operacao.QuantidadeOperacao = carteira.Quantidade;
                }

                if (operacao.QuantidadeOperacao <= carteira.Quantidade || operacao.QuantidadeOperacao >= 0) 
                {   
                    await AtualizarCarteiraEstoqueVenda(oferta, carteira, operacao);
                } else
                {
                    return resultado;
                }

            }
            resultado = await operacaoFacade.Inserir(operacao);

            return resultado;
        }
        private async Task AtualizarCarteiraEstoqueVenda(Oferta oferta, Investimentos carteira, Operacao operacao)
        {
            oferta.QuantidadeDisponivel += operacao.QuantidadeOperacao;
            await ofertaFacade.Alterar(oferta);

            if(carteira.Quantidade == operacao.QuantidadeOperacao)
            {
                await carteiraFacade.ExcluirPosicao(carteira.Id);
            } else
            {
                carteira.Quantidade -= operacao.QuantidadeOperacao;
                carteira.ValorTotalOperacao -= operacao.ValorTotalOperacao;
                await carteiraFacade.AlterarPosicao(carteira);
            }
        }

        private async Task AtualizarCarteiraEstoqueCompra(Oferta oferta, Operacao operacao, Cliente cliente)
        {
            oferta.QuantidadeDisponivel -= operacao.QuantidadeOperacao;
            await ofertaFacade.Alterar(oferta);

            var posicaoExistente = carteiraRepository.BuscarCarteiraCliente(oferta.CodigoOferta, cliente.Id);

            if (posicaoExistente == null)
            {
                Investimentos carteira = new Investimentos
                {
                    IdCliente = cliente.Id,
                    NomeCliente = cliente.Nome,
                    CodigoOferta = oferta.CodigoOferta,
                    Papel = oferta.Papel,
                    Quantidade = operacao.QuantidadeOperacao,
                    PrecoUnitario = oferta.PrecoUnitario,
                    ValorTotalOperacao = operacao.ValorTotalOperacao,
                    DataVencimento = oferta.DataVencimento
                };
                await carteiraFacade.IncluirPosicao(carteira);
            }
            else
            {
                posicaoExistente.Quantidade += operacao.QuantidadeOperacao;
                posicaoExistente.ValorTotalOperacao += operacao.ValorTotalOperacao;
                await carteiraFacade.AlterarPosicao(posicaoExistente);
            }
        }
    }
}
