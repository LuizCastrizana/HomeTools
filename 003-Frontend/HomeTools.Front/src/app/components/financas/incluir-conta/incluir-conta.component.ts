import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { TipoContaEnum } from 'src/app/enums/tipoContaEnum';
import { CreateContaDto } from 'src/app/interfaces/api-dto/financas/createContaDto';
import { CreateContaVariavelDto } from 'src/app/interfaces/api-dto/financas/createContaVariavelDto';
import { ContaVariavelService } from 'src/app/services/Financas/conta-variavel.service';
import { ContaService } from 'src/app/services/Financas/conta.service';
import { CategoriaService } from 'src/app/services/categoria.service';
import { ValidacaoService } from 'src/app/services/validacao.service';

@Component({
  selector: 'app-incluir-conta',
  templateUrl: './incluir-conta.component.html',
  styleUrls: ['./incluir-conta.component.css']
})
export class IncluirContaComponent implements OnInit {

  Conta: CreateContaDto = { } as CreateContaDto;

  TipoContaId: number = 0;
  Categorias$ = this.serviceCategoria.listar();

  constructor(
    private serviceConta: ContaService,
    private serviceContaVariavel: ContaVariavelService,
    private serviceCategoria: CategoriaService,
    private serviceValidacao: ValidacaoService,
    private router: Router
    ) { }

  ngOnInit(): void {
    this.Conta.CategoriaId = 0;
  }

  selecionaTipoConta() {
    let tipoConta = (<HTMLInputElement>document.getElementById("selTipoConta")).value;
    if (tipoConta == TipoContaEnum.Fixa.toLocaleString()) {
      document.getElementById("divValor")!.style.display = "flex";
    } else {
      document.getElementById("divValor")!.style.display = "none";
    }
  }

  salvarConta() {
    if (this.TipoContaId == TipoContaEnum.Fixa) {
      this.serviceConta.incluir(this.Conta).subscribe(() => {
        this.router.navigate(['/contas']);
      });
    }
    else if (this.TipoContaId == TipoContaEnum.Variavel){
      let contaVariavel: CreateContaVariavelDto = {
        Descricao: this.Conta.Descricao,
        DiaVencimento: this.Conta.DiaVencimento,
        CategoriaId: this.Conta.CategoriaId
      };
      this.serviceContaVariavel.incluir(contaVariavel).subscribe(() => {
        this.router.navigate(['/contas']);
      });
    }
  }

  cancelar() {
    this.router.navigate(['/contas']);
  }

  apenasNumeros(event: any) {
    if (!this.serviceValidacao.apenasNumeros(event)) {
      event.preventDefault();
    }
  }

  diaMes(event: any) {
    if (!this.serviceValidacao.diaMes(event)) {
      event.preventDefault();
    }
  }
}
