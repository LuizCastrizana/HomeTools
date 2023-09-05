import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { CreateContaDto } from 'src/app/interfaces/api-dto/financas/createContaDto';
import { CreateContaVariavelDto } from 'src/app/interfaces/api-dto/financas/createContaVariavelDto';
import { ContaVariavelService } from 'src/app/services/Financas/conta-variavel.service';
import { ContaService } from 'src/app/services/Financas/conta.service';
import { CategoriaService } from 'src/app/services/categoria.service';

@Component({
  selector: 'app-incluir-conta',
  templateUrl: './incluir-conta.component.html',
  styleUrls: ['./incluir-conta.component.css']
})
export class IncluirContaComponent implements OnInit {

  constructor(
    private serviceConta: ContaService,
    private serviceContaVariavel: ContaVariavelService,
    private serviceCategoria: CategoriaService,
    private router: Router
    ) { }

  Conta: CreateContaDto = {
    Descricao: '',
    ValorInteiro: 0,
    ValorCentavos: 0,
    DiaVencimento: 0,
    CategoriaId: 0
  }
  TipoContaId: number = 0;
  Categorias$ = this.serviceCategoria.listar();

  ngOnInit(): void {
  }

  selecionaTipoConta() {
    let tipoConta = (<HTMLInputElement>document.getElementById("tipoConta")).value;
    if (tipoConta == '1') {
      document.getElementById("divValor")!.style.display = "flex";
    } else {
      document.getElementById("divValor")!.style.display = "none";
    }
  }

  salvarConta() {
    if (this.TipoContaId == 1) {
      this.serviceConta.incluir(this.Conta).subscribe(() => {
        this.router.navigate(['/contas']);
      });
    }
    else if (this.TipoContaId == 2){
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
}
