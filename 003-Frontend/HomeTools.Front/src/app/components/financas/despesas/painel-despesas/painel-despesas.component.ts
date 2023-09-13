import { Component, OnInit } from '@angular/core';
import { TipoAlertaEnum } from 'src/app/enums/tipoAlertaEnum';
import { DadosFeedbackAlerta } from 'src/app/interfaces/dadosFeedbackAlerta';
import { Despesa } from 'src/app/interfaces/financas/Despesa';
import { DadosPaginador } from 'src/app/interfaces/paginacao/dadosPaginador';
import { DadosPaginados } from 'src/app/interfaces/paginacao/dadosPaginados';
import { ItemPagina } from 'src/app/interfaces/paginacao/itemPagina';
import { DespesaMapper } from 'src/app/mappers/financas/DespesaMapper';
import { DespesaService } from 'src/app/services/Financas/despesa.service';
import { FeedbackService } from 'src/app/services/feedback.service';
import { RespostaApiService } from 'src/app/services/resposta-api.service';

@Component({
  selector: 'app-painel-despesas',
  templateUrl: './painel-despesas.component.html',
  styleUrls: ['./painel-despesas.component.css']
})
export class PainelDespesasComponent implements OnInit {

  Ordem: string = '';
  NomeCampo: string = '';

  DadosPaginador: DadosPaginador = {
    PaginaAtual: 1,
    ItensPorPagina: 15,
    TotalItens: 0,
  };

  DadosPaginados: DadosPaginados<Despesa> = { } as DadosPaginados<Despesa>;
  DespesaAcao: Despesa = { } as Despesa;
  Despesas: Despesa[] = [];

  constructor(
    private despesaService: DespesaService,
    private feedbackService: FeedbackService,
    private respostaApiService: RespostaApiService,
    private despesaMapper: DespesaMapper
    ) {}

  ngOnInit(): void {
    this.despesaService.listar().subscribe({
      next: (respostaApi) => {
        respostaApi.valor.forEach(despesaDto => {
          this.Despesas.push(this.despesaMapper.DespesaDtoToDespesa(despesaDto));
        });
        this.ordenarDespesas();
      }, error: (err) => {
        let dadosFeedback = {
          Id: "feedback1",
          TipoAlerta: TipoAlertaEnum.Erro,
          Titulo: "Erro!",
          Mensagem: "Não foi possível obter as Despesas."
        } as DadosFeedbackAlerta;
        this.feedbackService.gerarFeedbackAlerta(dadosFeedback);
      }
    });
  }

  buscarDespesas(): void {
    let busca = (document.getElementById('txtBusca') as HTMLInputElement).value;
    this.Despesas = [];
    this.despesaService.buscar(busca).subscribe({
      next: (respostaApi) => {
        respostaApi.valor.forEach(despesaDto => {
          this.Despesas.push(this.despesaMapper.DespesaDtoToDespesa(despesaDto));
        });
        this.ordenarDespesas();
      }, error: (err) => {
        this.respostaApiService.tratarRespostaApi(err);
      }
    });
  }

  ordenarDespesas(nomeCampo?: string): void {
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
      case 'Descricao':
        this.NomeCampo = 'Descricao';
        if (this.Ordem == 'asc') {
          this.Despesas.sort((a, b) =>
            a.Descricao.localeCompare(b.Descricao, 'pt-BR')
          );
        } else {
          this.Ordem = 'desc';
          this.Despesas.sort((a, b) =>
            b.Descricao.localeCompare(a.Descricao, 'pt-BR')
          );
        }
        break;
      case 'Categoria':
        this.NomeCampo = 'Categoria';
        if (this.Ordem == 'asc') {
          this.Despesas.sort((a, b) =>
            a.Categoria.Descricao.localeCompare(b.Categoria.Descricao, 'pt-BR')
          );
        } else {
          this.Ordem = 'desc';
          this.Despesas.sort((a, b) =>
            b.Categoria.Descricao.localeCompare(a.Categoria.Descricao, 'pt-BR')
          );
        }
        break;
      case 'Valor':
        this.NomeCampo = 'Valor';
        if (this.Ordem == 'asc') {
          this.Despesas.sort((a, b) => a.ValorInteiro - b.ValorInteiro);
        } else {
          this.Ordem = 'desc';
          this.Despesas.sort((a, b) => b.ValorInteiro - a.ValorInteiro);
        }
        break;
      default:
        this.NomeCampo = '';
        this.Despesas.sort((a, b) => b.Id - a.Id);
        break;
    }
    this.tratarIconeOrdenacao();
    this.DadosPaginador.PaginaAtual = 1;
    this.paginarDespesas();
  }

  tratarIconeOrdenacao() {
    let imgAsc = "<img src=\"../../../../assets/img/asc.png\" >";
    let imgDesc = "<img src=\"../../../../assets/img/desc.png\" >";
    let imgDescricao = document.getElementById('imgDescricao');
    let imgCategoria = document.getElementById('imgCategoria');
    let imgValor = document.getElementById('imgValor');

    imgDescricao!.innerHTML = '';
    imgCategoria!.innerHTML = '';
    imgValor!.innerHTML = '';

    switch (this.NomeCampo) {
      case 'Descricao':
        if (this.Ordem == 'asc') {
          imgDescricao!.innerHTML = imgAsc;
        } else {
          imgDescricao!.innerHTML = imgDesc;
        }
        break;
      case 'Categoria':
        if (this.Ordem == 'asc') {
          imgCategoria!.innerHTML = imgAsc;
        } else {
          imgCategoria!.innerHTML = imgDesc;
        }
        break;
      case 'Valor':
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

  receberDadosPaginador($event: DadosPaginador): void {
    this.DadosPaginador = $event;
    this.paginarDespesas();
  }

  paginarDespesas() {
    this.DadosPaginados.Pagina = this.DadosPaginador.PaginaAtual;
    this.DadosPaginados.ItensPorPagina = this.DadosPaginador.ItensPorPagina;
    this.DadosPaginados.TotalItens = this.DadosPaginador.TotalItens;

    let itensPagina: ItemPagina<Despesa>[] = [];
    let pagina = 1;
    this.Despesas.forEach(item => {
      itensPagina.push({  Pagina: pagina, Item: item });
      if (itensPagina.length == this.DadosPaginados.ItensPorPagina * pagina) {
        pagina++;
      }
    });

    this.DadosPaginados.Itens = itensPagina.filter(item => item.Pagina == this.DadosPaginados.Pagina).map(item => item.Item);
  }

  exibirModalExcluir(despesa: Despesa): void {
    this.DespesaAcao = despesa;
    document.getElementById('modalExcluir')!.style.display = 'block';
  }
}
