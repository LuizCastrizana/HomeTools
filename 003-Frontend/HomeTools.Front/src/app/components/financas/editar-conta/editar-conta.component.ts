import { Categoria } from './../../../interfaces/categoria';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Observable } from 'rxjs';
import { StatusContaEnum } from 'src/app/enums/statusContaEnum';
import { TipoContaEnum } from 'src/app/enums/tipoContaEnum';
import { UpdateContaDto } from 'src/app/interfaces/api-dto/financas/updateContaDto';
import { UpdateContaVariavelDto } from 'src/app/interfaces/api-dto/financas/updateContaVariavelDto';
import { RespostaApi } from 'src/app/interfaces/api-dto/respostaApi';
import { ReadConta } from 'src/app/interfaces/financas/readConta';
import { PagamentoConta } from 'src/app/interfaces/financas/readPagamentoConta';
import { ContaVariavelService } from 'src/app/services/Financas/conta-variavel.service';
import { ContaService } from 'src/app/services/Financas/conta.service';
import { CategoriaService } from 'src/app/services/categoria.service';

@Component({
  selector: 'app-editar-conta',
  templateUrl: './editar-conta.component.html',
  styleUrls: ['./editar-conta.component.css']
})
export class EditarContaComponent implements OnInit {

  constructor(
    private serviceConta: ContaService,
    private serviceContaVariavel: ContaVariavelService,
    private serviceCategoria: CategoriaService,
    private router: Router,
    private route: ActivatedRoute,
  ) { }

  Conta: ReadConta = {
    Id: 0,
    Descricao: '',
    ValorInteiro: 0,
    ValorCentavos: 0,
    DiaVencimento: 0,
    Categoria: {} as Categoria,
    Pagamentos: [],
    UltimoPagamento: undefined,
    Variavel: false,
    StatusId: StatusContaEnum.Pendente,
  };

  TipoContaId: number = 0;
  Categorias$: Observable<RespostaApi<Categoria[]>> = {} as Observable<RespostaApi<Categoria[]>>;

  ngOnInit(): void {
    const id = this.route.snapshot.paramMap.get('id');
    const tipo = this.route.snapshot.paramMap.get('tipo');
    this.Categorias$ = this.serviceCategoria.listar()
    if (id != null) {
      if (tipo == TipoContaEnum.Variavel.toLocaleString()) {
        this.TipoContaId = TipoContaEnum.Variavel;
        this.serviceContaVariavel.buscarPorId(id!).subscribe(contaDto => {
          this.Conta = {
            Id: contaDto.Valor.Id,
            Descricao: contaDto.Valor.Descricao,
            ValorInteiro: 0,
            ValorCentavos: 0,
            DiaVencimento: contaDto.Valor.DiaVencimento,
            Categoria: contaDto.Valor.Categoria,
            Pagamentos: [],
            UltimoPagamento: undefined,
            Variavel: true,
            StatusId: StatusContaEnum.Pendente,
          };
          contaDto.Valor.Pagamentos.forEach(pagamentoDto => {
            let pagamento: PagamentoConta = {
              Id: pagamentoDto.Id,
              ValorInteiro: pagamentoDto.ValorInteiro,
              ValorCentavos: pagamentoDto.ValorCentavos,
              DataPagamento: pagamentoDto.DataPagamento,
              ContaId: pagamentoDto.ContaId
            }
            this.Conta.Pagamentos.push(pagamento);
          });
          if (this.Conta.Pagamentos.length > 0) {
            this.Conta.UltimoPagamento = this.Conta.Pagamentos.sort((a, b) => b.Id - a.Id)[0].DataPagamento;
            let UltimoPagamento = new Date(this.Conta.UltimoPagamento != undefined ? this.Conta.UltimoPagamento : 0);
            if (UltimoPagamento.getMonth() == new Date(Date.now()).getMonth()) {
              this.Conta.StatusId = StatusContaEnum.Paga;
            }
            else if (UltimoPagamento.getMonth() < new Date(Date.now()).getMonth()
              && this.Conta.DiaVencimento < new Date(Date.now()).getDay()) {
                this.Conta.StatusId = StatusContaEnum.Atrasada;
            }
            else if (UltimoPagamento.getMonth() < new Date(Date.now()).getMonth() -1) {
              this.Conta.StatusId = StatusContaEnum.Atrasada;
            }
          }
        });
      }
      else if (tipo == TipoContaEnum.Fixa.toLocaleString()) {
        this.TipoContaId = TipoContaEnum.Fixa;
        this.serviceConta.buscarPorId(id!).subscribe(respostaAPI => {
          this.Conta = {
            Id: respostaAPI.Valor.Id,
            Descricao: respostaAPI.Valor.Descricao,
            ValorInteiro: respostaAPI.Valor.ValorInteiro,
            ValorCentavos: respostaAPI.Valor.ValorCentavos,
            DiaVencimento: respostaAPI.Valor.DiaVencimento,
            Categoria: respostaAPI.Valor.Categoria,
            Pagamentos: [],
            UltimoPagamento: undefined,
            Variavel: false,
            StatusId: StatusContaEnum.Pendente,
          };
          respostaAPI.Valor.Pagamentos.forEach(pagamentoDto => {
            let pagamento: PagamentoConta = {
              Id: pagamentoDto.Id,
              ValorInteiro: 0,
              ValorCentavos: 0,
              DataPagamento: pagamentoDto.DataPagamento,
              ContaId: pagamentoDto.ContaId
            }
            this.Conta.Pagamentos.push(pagamento);
          });
          if (this.Conta.Pagamentos.length > 0) {
            this.Conta.UltimoPagamento = this.Conta.Pagamentos.sort((a, b) => b.Id - a.Id)[0].DataPagamento;
            let UltimoPagamento = new Date(this.Conta.UltimoPagamento != undefined ? this.Conta.UltimoPagamento : 0);
            if (UltimoPagamento.getMonth() == new Date(Date.now()).getMonth()) {
              this.Conta.StatusId = StatusContaEnum.Paga;
            }
            else if (UltimoPagamento.getMonth() < new Date(Date.now()).getMonth()
              && this.Conta.DiaVencimento < new Date(Date.now()).getDay()) {
                this.Conta.StatusId = StatusContaEnum.Atrasada;
            }
            else if (UltimoPagamento.getMonth() < new Date(Date.now()).getMonth() -1) {
              this.Conta.StatusId = StatusContaEnum.Atrasada;
            }
          }
        });
      }
    }
    if (this.TipoContaId == TipoContaEnum.Variavel) {
      document.getElementById("divValor")!.style.display = "none";
    }
  }

  selecionaTipoConta() {
    let tipoConta = (<HTMLInputElement>document.getElementById("tipoConta")).value;
    if (tipoConta == TipoContaEnum.Fixa.toLocaleString()) {
      document.getElementById("divValor")!.style.display = "flex";
    } else {
      document.getElementById("divValor")!.style.display = "none";
    }
  }

  salvarConta() {
    if (this.TipoContaId == TipoContaEnum.Fixa) {
      let conta: UpdateContaDto = {
        Descricao: this.Conta.Descricao,
        ValorInteiro: this.Conta.ValorInteiro,
        ValorCentavos: this.Conta.ValorCentavos,
        DiaVencimento: this.Conta.DiaVencimento,
        CategoriaId: this.Conta.Categoria.Id
      };
      this.serviceConta.atualizar(this.Conta.Id.toLocaleString(), conta).subscribe(() => {
        this.router.navigate(['/contas']);
      });
    }
    else if (this.TipoContaId == TipoContaEnum.Variavel){
      let contaVariavel: UpdateContaVariavelDto = {
        Descricao: this.Conta.Descricao,
        DiaVencimento: this.Conta.DiaVencimento,
        CategoriaId: this.Conta.Categoria.Id
      };
      this.serviceContaVariavel.atualizar(this.Conta.Id.toLocaleString(), contaVariavel).subscribe(() => {
        this.router.navigate(['/contas']);
      });
    }
  }
  cancelar() {
    this.router.navigate(['/contas']);
  }
}
