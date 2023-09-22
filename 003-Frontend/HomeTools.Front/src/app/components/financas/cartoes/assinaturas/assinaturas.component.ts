import { DadosFeedbackAlerta } from 'src/app/interfaces/dadosFeedbackAlerta';
import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { Assinatura } from 'src/app/interfaces/financas/Assinatura';
import { Cartao } from 'src/app/interfaces/financas/Cartao';
import { AssinaturaMapper } from 'src/app/mappers/financas/AssinaturaMapper';
import { AssinaturaService } from 'src/app/services/Financas/cartao/assinatura.service';
import { FeedbackService } from 'src/app/services/feedback.service';
import { TipoAlertaEnum } from 'src/app/enums/tipoAlertaEnum';
import { DadosPaginador } from 'src/app/interfaces/paginacao/dadosPaginador';
import { DadosPaginados } from 'src/app/interfaces/paginacao/dadosPaginados';
import { ItemPagina } from 'src/app/interfaces/paginacao/itemPagina';
import { ReadAssinaturaDto } from 'src/app/dto/financas/cartoes/readAssinaturaDto';
import { RespostaApiService } from 'src/app/services/resposta-api.service';
import { DadosModalExcluir } from 'src/app/interfaces/dadosModalExcluir';

@Component({
  selector: 'app-assinaturas',
  templateUrl: './assinaturas.component.html',
  styleUrls: ['./assinaturas.component.css']
})
export class AssinaturasComponent implements OnInit {

  @Input() Cartao = {} as Cartao;
  @Output() CartaoChange = new EventEmitter<Cartao>();
  AcaoEditar: boolean = false;
  AssinaturaAcao: Assinatura = {} as Assinatura;
  DadosModalExcluir: DadosModalExcluir = {} as DadosModalExcluir;

  Ordem: string = '';
  NomeCampo: string = '';

  DadosPaginador: DadosPaginador = {
    PaginaAtual: 1,
    ItensPorPagina: 5,
    TotalItens: 0,
  };

  DadosPaginados: DadosPaginados<Assinatura> = { } as DadosPaginados<Assinatura>;

  constructor(
    private assinaturaService: AssinaturaService,
    private feedbackService: FeedbackService,
    private respostaApiService: RespostaApiService,
  ) { }

  ngOnInit(): void {
    this.listarAssinaturas();
    this.CartaoChange.emit(this.Cartao);
  }

  ngOnChanges(): void {
    this.listarAssinaturas();
    this.CartaoChange.emit(this.Cartao);
  }

  listarAssinaturas() {
    this.assinaturaService.buscarPorCartaoId(this.Cartao.Id.toString()).subscribe({
      next: (retornoDaApi) => {
        this.Cartao.Assinaturas = [];
        retornoDaApi.valor.forEach((assinatura) => {
          this.Cartao.Assinaturas.push(AssinaturaMapper.AssinaturaDtoToAssinatura(assinatura));
        });
        this.ordenarAssinaturas();
      }, error: (error) => {
        let dadosFeedback: DadosFeedbackAlerta = {
          Id: 'feedback',
          TipoAlerta: TipoAlertaEnum.Erro,
          Titulo: 'Erro!',
          Mensagem: 'Erro ao listar assinaturas.'
        };
        this.feedbackService.gerarFeedbackAlerta(dadosFeedback);
      }
    });
  }

  buscarAssinaturas() {
    let busca = (document.getElementById('txtBuscaAssinaturas') as HTMLInputElement).value;
    this.Cartao.Assinaturas = [];
    this.assinaturaService.buscar(busca).subscribe({
      next: (respostaApi) => {
        respostaApi.valor.forEach((AssinaturaDto: ReadAssinaturaDto) => {
          this.Cartao.Assinaturas.push(AssinaturaMapper.AssinaturaDtoToAssinatura(AssinaturaDto));
        });
        this.ordenarAssinaturas();
      }, error: (err) => {
        this.respostaApiService.tratarRespostaApi(err);
      }
    });
  }

  ordenarAssinaturas(nomeCampo?: string) {
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
      case 'DescricaoAssinatura':
        this.NomeCampo = 'DescricaoAssinatura';
        if (this.Ordem == 'asc') {
          this.Cartao.Assinaturas.sort((a, b) => a.Descricao.localeCompare(b.Descricao, 'pt-BR'));
        } else {
          this.Ordem = 'desc';
          this.Cartao.Assinaturas.sort((a, b) => b.Descricao.localeCompare(a.Descricao, 'pt-BR'));
        }
        break;
      case 'DiaVencimentoAssinatura':
        this.NomeCampo = 'DiaVencimentoAssinatura';
        if (this.Ordem == 'asc') {
          this.Cartao.Assinaturas.sort((a, b) => a.DiaVencimento - b.DiaVencimento);
        } else {
          this.Ordem = 'desc';
          this.Cartao.Assinaturas.sort((a, b) => b.DiaVencimento - a.DiaVencimento);
        }
        break;
      case 'ValorAssinatura':
        this.NomeCampo = 'ValorAssinatura';
        if (this.Ordem == 'asc') {
          this.Cartao.Assinaturas.sort((a, b) => a.ValorInteiro - b.ValorInteiro && a.ValorCentavos - b.ValorCentavos);
        } else {
          this.Ordem = 'desc';
          this.Cartao.Assinaturas.sort((a, b) => b.ValorInteiro - a.ValorInteiro && b.ValorCentavos - a.ValorCentavos);
        }
        break;
      default:
        this.NomeCampo = '';
        this.Cartao.Assinaturas.sort((a, b) => b.Id - a.Id);
        break;
      }
    this.tratarIconeOrdenacao();
    this.DadosPaginador.PaginaAtual = 1;
    this.paginarAssinaturas();
  }

  tratarIconeOrdenacao() {
    let imgAsc = "<img src=\"../../../../assets/img/asc.png\" >";
    let imgDesc = "<img src=\"../../../../assets/img/desc.png\" >";
    let imgDescricao = document.getElementById('imgDescricaoAssinatura');
    let imgDiaVencimento = document.getElementById('imgDiaVencimentoAssinatura');
    let imgValor = document.getElementById('imgValorAssinatura');

    imgDescricao!.innerHTML = '';
    imgDiaVencimento!.innerHTML = '';
    imgValor!.innerHTML = '';

    switch (this.NomeCampo) {
      case 'DescricaoAssinatura':
        if (this.Ordem == 'asc') {
          imgDescricao!.innerHTML = imgAsc;
        } else {
          imgDescricao!.innerHTML = imgDesc;
        }
        break;
      case 'DiaVencimentoAssinatura':
        if (this.Ordem == 'asc') {
          imgDiaVencimento!.innerHTML = imgAsc;
        } else {
          imgDiaVencimento!.innerHTML = imgDesc;
        }
        break;
      case 'ValorAssinatura':
        if (this.Ordem == 'asc') {
          imgValor!.innerHTML = imgAsc;
        }
        else {
          imgValor!.innerHTML = imgDesc;
        }
        break;
      default:
        break;
      }
  }

  paginarAssinaturas() {
    this.DadosPaginados.Pagina = this.DadosPaginador.PaginaAtual;
    this.DadosPaginados.ItensPorPagina = this.DadosPaginador.ItensPorPagina;
    this.DadosPaginados.TotalItens = this.DadosPaginador.TotalItens;

    let itensPagina: ItemPagina<Assinatura>[] = [];
    let pagina = 1;
    this.Cartao.Assinaturas.forEach(item => {
      itensPagina.push({  Pagina: pagina, Item: item });
      if (itensPagina.length == this.DadosPaginados.ItensPorPagina * pagina) {
        pagina++;
      }
    });

    this.DadosPaginados.Itens = itensPagina.filter(itemPagina => itemPagina.Pagina == this.DadosPaginados.Pagina).map(itemPagina => itemPagina.Item);
  }

  exibirModalExcluir(AssinaturaAcao: Assinatura) {
    this.AssinaturaAcao = AssinaturaAcao
    this.DadosModalExcluir = {
      IdRegistro: AssinaturaAcao.Id,
      NomeRegistro: AssinaturaAcao.Descricao,
    } as DadosModalExcluir;
    document.getElementById('modalExcluir')!.style.display = 'block';
  }

  exibirModalFormulario(Edicao: boolean, AssinaturaAcao?: Assinatura) {
    this.AssinaturaAcao = AssinaturaAcao != undefined ? AssinaturaAcao : ({} as Assinatura);
    this.AcaoEditar = Edicao;
    document.getElementById('modalFormularioAssinatura')!.style.display = 'block';
  }

  receiveMessage($event: DadosPaginador) {
    this.DadosPaginador = $event
    this.paginarAssinaturas();
  }

  receiveMessageExcluir($event: DadosModalExcluir) {
    this.DadosModalExcluir = $event;
    this.assinaturaService.excluir(this.DadosModalExcluir.IdRegistro.toString()).subscribe({
      next: (retornoApi) => {
        this.respostaApiService.tratarRespostaApi(retornoApi);
        this.listarAssinaturas();
        document.getElementById('modalExcluir')!.style.display = 'none';
        window.scroll(0,0);
      },
      error: (err) => {
        this.respostaApiService.tratarRespostaApi(err);
        document.getElementById('modalExcluir')!.style.display = 'none';
        window.scroll(0,0);
      }
    });
  }

  receiveMessageFormulario($eventEdicao: boolean) {
    if ($eventEdicao) {
      this.assinaturaService
          .atualizar(
            this.AssinaturaAcao!.Id.toString(),
            AssinaturaMapper.AssinaturaToCreateAssinaturaDto(this.AssinaturaAcao!)
          )
          .subscribe({
            next: (result) => {
              this.respostaApiService.tratarRespostaApi(result);
              this.listarAssinaturas();
            },
            error: (err) => {
              this.respostaApiService.tratarRespostaApi(err);
            }
          });
    } else {
      this.assinaturaService
          .incluir(AssinaturaMapper.AssinaturaToCreateAssinaturaDto(this.AssinaturaAcao!))
          .subscribe({
            next: (result) => {
              this.respostaApiService.tratarRespostaApi(result);
              this.listarAssinaturas();
            },
            error: (err) => {
              this.respostaApiService.tratarRespostaApi(err);
            }
          });
    }
    document.getElementById('modalFormularioAssinatura')!.style.display = 'none';
    window.scroll(0,0);
  }
}
