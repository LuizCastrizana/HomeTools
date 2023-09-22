import { DadosModalExcluir } from './../../../../interfaces/dadosModalExcluir';
import { DadosFeedbackAlerta } from 'src/app/interfaces/dadosFeedbackAlerta';
import { CompraService } from './../../../../services/Financas/cartao/compra.service';
import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { Cartao } from 'src/app/interfaces/financas/Cartao';
import { Compra } from 'src/app/interfaces/financas/Compra';
import { DadosPaginador } from 'src/app/interfaces/paginacao/dadosPaginador';
import { DadosPaginados } from 'src/app/interfaces/paginacao/dadosPaginados';
import { ItemPagina } from 'src/app/interfaces/paginacao/itemPagina';
import { CompraMapper } from 'src/app/mappers/financas/CompraMapper';
import { FeedbackService } from 'src/app/services/feedback.service';
import { RespostaApiService } from 'src/app/services/resposta-api.service';
import { TipoAlertaEnum } from 'src/app/enums/tipoAlertaEnum';
import { ReadCompraDto } from 'src/app/dto/financas/cartoes/readCompraDto';
import { Router } from '@angular/router';

@Component({
  selector: 'app-compras',
  templateUrl: './compras.component.html',
  styleUrls: ['./compras.component.css'],
})
export class ComprasComponent implements OnInit {
  @Input() Cartao = {} as Cartao;
  @Output() CartaoChange = new EventEmitter<Cartao>();
  CompraAcao?: Compra = {} as Compra;
  AcaoEditar: boolean = false;

  Ordem: string = '';
  NomeCampo: string = '';

  DadosPaginador: DadosPaginador = {
    PaginaAtual: 1,
    ItensPorPagina: 5,
    TotalItens: 0,
  };

  DadosPaginados: DadosPaginados<Compra> = {} as DadosPaginados<Compra>;

  DadosModalExcluir: DadosModalExcluir = {} as DadosModalExcluir;

  constructor(
    private compraService: CompraService,
    private feedbackService: FeedbackService,
    private respostaApiService: RespostaApiService,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.listarCompras();
    this.CartaoChange.emit(this.Cartao);
  }

  ngOnChanges(): void {
    this.listarCompras();
    this.CartaoChange.emit(this.Cartao);
  }

  listarCompras() {
    this.compraService.buscarPorCartaoId(this.Cartao.Id.toString()).subscribe({
      next: (retornoDaApi) => {
        this.Cartao.Compras = [];
        retornoDaApi.valor.forEach((compra) => {
          this.Cartao.Compras.push(CompraMapper.CompraDtoToCompra(compra));
        });
        this.ordenarCompras();
      },
      error: (error) => {
        let dadosFeedback: DadosFeedbackAlerta = {
          Id: 'feedback',
          TipoAlerta: TipoAlertaEnum.Erro,
          Titulo: 'Erro!',
          Mensagem: 'Erro ao listar compras.',
        };
        this.feedbackService.gerarFeedbackAlerta(dadosFeedback);
      },
    });
  }

  buscarCompras() {
    let busca = (document.getElementById('txtBuscaCompra') as HTMLInputElement).value;
    this.Cartao.Compras = [];
    this.compraService.buscar(busca).subscribe({
      next: (respostaApi) => {
        respostaApi.valor.forEach((CompraDto: ReadCompraDto) => {
          this.Cartao.Compras.push(CompraMapper.CompraDtoToCompra(CompraDto));
        });
        this.ordenarCompras();
      },
      error: (err) => {
        this.respostaApiService.tratarRespostaApi(err);
      },
    });
  }

  ordenarCompras(nomeCampo?: string) {
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
      case 'DescricaoCompra':
        this.NomeCampo = 'DescricaoCompra';
        if (this.Ordem == 'asc') {
          this.Cartao.Compras.sort((a, b) =>
            a.Descricao.localeCompare(b.Descricao, 'pt-BR')
          );
        } else {
          this.Ordem = 'desc';
          this.Cartao.Compras.sort((a, b) =>
            b.Descricao.localeCompare(a.Descricao, 'pt-BR')
          );
        }
        break;
      case 'CategoriaCompra':
        this.NomeCampo = 'CategoriaCompra';
        if (this.Ordem == 'asc') {
          this.Cartao.Compras.sort((a, b) =>
            a.Categoria.Descricao.localeCompare(b.Categoria.Descricao, 'pt-BR')
          );
        } else {
          this.Ordem = 'desc';
          this.Cartao.Compras.sort((a, b) =>
            b.Categoria.Descricao.localeCompare(a.Categoria.Descricao, 'pt-BR')
          );
        }
        break;
      case 'DataCompra':
        this.NomeCampo = 'DataCompra';
        if (this.Ordem == 'asc') {
          this.Cartao.Compras.sort(
            (a, b) =>
              new Date(a.DataCompra).getTime() -
              new Date(b.DataCompra).getTime()
          );
        } else {
          this.Ordem = 'desc';
          this.Cartao.Compras.sort(
            (a, b) =>
              new Date(b.DataCompra).getTime() -
              new Date(a.DataCompra).getTime()
          );
        }
        break;
      case 'ValorCompra':
        this.NomeCampo = 'ValorCompra';
        if (this.Ordem == 'asc') {
          this.Cartao.Compras.sort(
            (a, b) =>
              a.ValorInteiro - b.ValorInteiro &&
              a.ValorCentavos - b.ValorCentavos
          );
        } else {
          this.Ordem = 'desc';
          this.Cartao.Compras.sort(
            (a, b) =>
              b.ValorInteiro - a.ValorInteiro &&
              b.ValorCentavos - a.ValorCentavos
          );
        }
        break;
      case 'ParcelasCompra':
        this.NomeCampo = 'ParcelasCompra';
        if (this.Ordem == 'asc') {
          this.Cartao.Compras.sort((a, b) => a.QtdParcelas - b.QtdParcelas);
        } else {
          this.Ordem = 'desc';
          this.Cartao.Compras.sort((a, b) => b.QtdParcelas - a.QtdParcelas);
        }
        break;
      default:
        this.NomeCampo = '';
        this.Cartao.Compras.sort((a, b) => b.Id - a.Id);
        break;
    }
    this.tratarIconeOrdenacao();
    this.DadosPaginador.PaginaAtual = 1;
    this.paginarCompras();
  }

  tratarIconeOrdenacao() {
    let imgAsc = '<img src="../../../../assets/img/asc.png" >';
    let imgDesc = '<img src="../../../../assets/img/desc.png" >';
    let imgDescricao = document.getElementById('imgDescricaoCompra');
    let imgCategoria = document.getElementById('imgCategoriaCompra');
    let imgDataCompra = document.getElementById('imgDataCompra');
    let imgValor = document.getElementById('imgValorCompra');
    let imgParcelas = document.getElementById('imgParcelasCompra');

    imgDescricao!.innerHTML = '';
    imgCategoria!.innerHTML = '';
    imgDataCompra!.innerHTML = '';
    imgValor!.innerHTML = '';
    imgParcelas!.innerHTML = '';

    switch (this.NomeCampo) {
      case 'DescricaoCompra':
        if (this.Ordem == 'asc') {
          imgDescricao!.innerHTML = imgAsc;
        } else {
          imgDescricao!.innerHTML = imgDesc;
        }
        break;
      case 'CategoriaCompra':
        if (this.Ordem == 'asc') {
          imgCategoria!.innerHTML = imgAsc;
        } else {
          imgCategoria!.innerHTML = imgDesc;
        }
        break;
      case 'DataCompra':
        if (this.Ordem == 'asc') {
          imgDataCompra!.innerHTML = imgAsc;
        } else {
          imgDataCompra!.innerHTML = imgDesc;
        }
        break;
      case 'ValorCompra':
        if (this.Ordem == 'asc') {
          imgValor!.innerHTML = imgAsc;
        } else {
          imgValor!.innerHTML = imgDesc;
        }
        break;
      case 'ParcelasCompra':
        if (this.Ordem == 'asc') {
          imgParcelas!.innerHTML = imgAsc;
        } else {
          imgParcelas!.innerHTML = imgDesc;
        }
        break;
      default:
        break;
    }
  }

  paginarCompras() {
    this.DadosPaginados.Pagina = this.DadosPaginador.PaginaAtual;
    this.DadosPaginados.ItensPorPagina = this.DadosPaginador.ItensPorPagina;
    this.DadosPaginados.TotalItens = this.DadosPaginador.TotalItens;

    let itensPagina: ItemPagina<Compra>[] = [];
    let pagina = 1;
    this.Cartao.Compras.forEach((item) => {
      itensPagina.push({ Pagina: pagina, Item: item });
      if (itensPagina.length == this.DadosPaginados.ItensPorPagina * pagina) {
        pagina++;
      }
    });

    this.DadosPaginados.Itens = itensPagina
      .filter((itemPagina) => itemPagina.Pagina == this.DadosPaginados.Pagina)
      .map((itemPagina) => itemPagina.Item);
  }

  exibirModalFormulario(Edicao: boolean, CompraAcao?: Compra) {
    this.CompraAcao = CompraAcao != undefined ? CompraAcao : ({} as Compra);
    this.AcaoEditar = Edicao;
    document.getElementById('modalFormularioCompra')!.style.display = 'block';
  }
  exibirModalExcluir(CompraAcao: Compra) {
    this.CompraAcao = CompraAcao;
    this.DadosModalExcluir = {
      IdRegistro: CompraAcao.Id,
      NomeRegistro: CompraAcao.Descricao,
    };
    document.getElementById('modalExcluir')!.style.display = 'block';
  }

  receiveMessage($event: DadosPaginador) {
    this.DadosPaginador = $event;
    this.paginarCompras();
  }

  receiveMessageExcluir($event: DadosModalExcluir) {
    this.DadosModalExcluir = $event;
    this.compraService.excluir(this.DadosModalExcluir.IdRegistro.toString()).subscribe({
      next: (retornoApi) => {
        this.respostaApiService.tratarRespostaApi(retornoApi);
        this.listarCompras();
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
      this.compraService
          .atualizar(
            this.CompraAcao!.Id.toString(),
            CompraMapper.CompraToCreateCompraDto(this.CompraAcao!)
          )
          .subscribe({
            next: (result) => {
              this.respostaApiService.tratarRespostaApi(result);
              this.listarCompras();
            },
            error: (err) => {
              this.respostaApiService.tratarRespostaApi(err);
            }
          });
    } else {
      this.compraService
          .incluir(CompraMapper.CompraToCreateCompraDto(this.CompraAcao!))
          .subscribe({
            next: (result) => {
              this.respostaApiService.tratarRespostaApi(result);
              this.listarCompras();
            },
            error: (err) => {
              this.respostaApiService.tratarRespostaApi(err);
            }
          });
    }
    document.getElementById('modalFormularioCompra')!.style.display = 'none';
    window.scroll(0,0);
  }

}
