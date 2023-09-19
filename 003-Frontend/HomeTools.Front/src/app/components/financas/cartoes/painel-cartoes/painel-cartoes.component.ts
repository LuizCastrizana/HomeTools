import { CartaoMapper } from './../../../../mappers/financas/CartaoMapper';
import { Component, OnInit } from '@angular/core';
import { ReadCartaoDto } from 'src/app/dto/financas/cartoes/readCartaoDto';
import { TipoAlertaEnum } from 'src/app/enums/tipoAlertaEnum';
import { DadosFeedbackAlerta } from 'src/app/interfaces/dadosFeedbackAlerta';
import { Cartao } from 'src/app/interfaces/financas/Cartao';
import { DadosPaginador } from 'src/app/interfaces/paginacao/dadosPaginador';
import { DadosPaginados } from 'src/app/interfaces/paginacao/dadosPaginados';
import { ItemPagina } from 'src/app/interfaces/paginacao/itemPagina';
import { CartaoService } from 'src/app/services/Financas/cartao/cartao.service';
import { FeedbackService } from 'src/app/services/feedback.service';
import { RespostaApiService } from 'src/app/services/resposta-api.service';

@Component({
  selector: 'app-painel-cartoes',
  templateUrl: './painel-cartoes.component.html',
  styleUrls: ['./painel-cartoes.component.css']
})
export class PainelCartoesComponent implements OnInit {

  Ordem: string = '';
  NomeCampo: string = '';

  DadosPaginador: DadosPaginador = {
    PaginaAtual: 1,
    ItensPorPagina: 15,
    TotalItens: 0,
  };

  DadosPaginados: DadosPaginados<Cartao> = { } as DadosPaginados<Cartao>;
  CartaoAcao: Cartao = { } as Cartao;
  Cartoes: Cartao[] = [];

  constructor(
    private cartaoService: CartaoService,
    private feedbackService: FeedbackService,
    private respostaApiService: RespostaApiService,
  ) { }

  ngOnInit(): void {
    this.cartaoService.listar().subscribe({
      next: (respostaApi) => {
        respostaApi.valor.forEach((cartao: ReadCartaoDto) => {
          this.Cartoes.push(CartaoMapper.CartaoDtoToCartao(cartao));
        });
        this.ordenarCartoes();
      }, error: (err) => {
        let dadosFeedback = {
          Id: "feedback1",
          TipoAlerta: TipoAlertaEnum.Erro,
          Titulo: "Erro!",
          Mensagem: "Não foi possível obter os cartões."
        } as DadosFeedbackAlerta;
        this.feedbackService.gerarFeedbackAlerta(dadosFeedback);
      }
    });
  }

  buscarCartoes() {
    let busca = (document.getElementById('txtBusca') as HTMLInputElement).value;
    this.Cartoes = [];
    this.cartaoService.buscar(busca).subscribe({
      next: (respostaApi) => {
        respostaApi.valor.forEach((cartao: ReadCartaoDto) => {
          this.Cartoes.push(CartaoMapper.CartaoDtoToCartao(cartao));
        });
        this.ordenarCartoes();
      }, error: (err) => {
        this.respostaApiService.tratarRespostaApi(err);
      }
    });
  }

  ordenarCartoes(nomeCampo?: string) {
    if (nomeCampo == this.NomeCampo) {
      if (this.Ordem == 'desc') {
        this.Ordem = 'asc';
      } else if (this.Ordem == 'asc') {
        this.Ordem = '';
        this.NomeCampo = '';
        nomeCampo = '';
      }
    } else {
      this.Ordem = '';
    }

    switch (nomeCampo) {
      case 'Nome':
        this.NomeCampo = 'Nome';
        if (this.Ordem == 'asc') {
          this.Cartoes.sort((a, b) => a.Nome.localeCompare(b.Nome, 'pt-BR'));
        } else {
          this.Ordem = 'desc';
          this.Cartoes.sort((a, b) => b.Nome.localeCompare(a.Nome, 'pt-BR'));
        }
        break;
      case 'DiaVencimento':
        this.NomeCampo = 'DiaVencimento';
        if (this.Ordem == 'asc') {
          this.Cartoes.sort((a, b) => a.DiaVencimento - b.DiaVencimento);
        } else {
          this.Ordem = 'desc';
          this.Cartoes.sort((a, b) => b.DiaVencimento - a.DiaVencimento);
        }
        break;
      case 'DiaFechamento':
        this.NomeCampo = 'DiaFechamento';
        if (this.Ordem == 'asc') {
          this.Cartoes.sort((a, b) => a.DiaFechamento - b.DiaFechamento);
        } else {
          this.Ordem = 'desc';
          this.Cartoes.sort((a, b) => b.DiaFechamento - a.DiaFechamento);
        }
        break;
      default:
        this.NomeCampo = '';
        this.Cartoes.sort((a, b) => b.Id - a.Id);
        break;
      }
    this.tratarIconeOrdenacao();
    this.DadosPaginador.PaginaAtual = 1;
    this.paginarCartoes();
  }

  tratarIconeOrdenacao() {
    let imgAsc = "<img src=\"../../../../assets/img/asc.png\" >";
    let imgDesc = "<img src=\"../../../../assets/img/desc.png\" >";
    let imgNome = document.getElementById('imgNome');
    let imgDiaVencimento = document.getElementById('imgDiaVencimento');
    let imgDiaFechamento = document.getElementById('imgDiaFechamento');

    imgNome!.innerHTML = '';
    imgDiaVencimento!.innerHTML = '';
    imgDiaFechamento!.innerHTML = '';

    switch (this.NomeCampo) {
      case 'Nome':
        if (this.Ordem == 'asc') {
          imgNome!.innerHTML = imgAsc;
        } else {
          imgNome!.innerHTML = imgDesc;
        }
        break;
      case 'DiaVencimento':
        if (this.Ordem == 'asc') {
          imgDiaVencimento!.innerHTML = imgAsc;
        } else {
          imgDiaVencimento!.innerHTML = imgDesc;
        }
        break;
      case 'DiaFechamento':
        if (this.Ordem == 'asc') {
          imgDiaFechamento!.innerHTML = imgAsc;
        }
        else {
          imgDiaFechamento!.innerHTML = imgDesc;
        }
        break;
      default:
        break;
      }
  }

  paginarCartoes() {
    this.DadosPaginados.Pagina = this.DadosPaginador.PaginaAtual;
    this.DadosPaginados.ItensPorPagina = this.DadosPaginador.ItensPorPagina;
    this.DadosPaginados.TotalItens = this.DadosPaginador.TotalItens;

    let itensPagina: ItemPagina<Cartao>[] = [];
    let pagina = 1;
    this.Cartoes.forEach(item => {
      itensPagina.push({  Pagina: pagina, Item: item });
      if (itensPagina.length == this.DadosPaginados.ItensPorPagina * pagina) {
        pagina++;
      }
    });

    this.DadosPaginados.Itens = itensPagina.filter(item => item.Pagina == this.DadosPaginados.Pagina).map(item => item.Item);
  }

  exibirModalExcluir(Cartao: Cartao) {
    this.CartaoAcao = Cartao
    document.getElementById('modalExcluirCartao')!.style.display = 'block';
  }

  receiveMessage($event: DadosPaginador) {
    this.DadosPaginador = $event
    this.paginarCartoes();
  }

}
