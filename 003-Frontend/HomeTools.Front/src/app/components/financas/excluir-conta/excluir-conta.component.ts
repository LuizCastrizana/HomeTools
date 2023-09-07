import { Component, Input, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { ReadConta } from 'src/app/interfaces/financas/readConta';
import { ContaVariavelService } from 'src/app/services/Financas/conta-variavel.service';
import { ContaService } from 'src/app/services/Financas/conta.service';

@Component({
  selector: 'app-excluir-conta',
  templateUrl: './excluir-conta.component.html',
  styleUrls: ['./excluir-conta.component.css']
})
export class ExcluirContaComponent implements OnInit {

  @Input() Conta: ReadConta = {} as ReadConta;

  constructor(
    private serviceConta: ContaService,
    private serviceContaVariavel: ContaVariavelService,
    private router: Router,
    private route: ActivatedRoute,
    ) { }

  ngOnInit(): void {
  }

  excluirConta() {
    if (this.Conta.Variavel) {
      this.serviceContaVariavel.excluir(this.Conta.Id.toString()).subscribe(() => {
        document.getElementById('modalExcluir')!.style.display = 'none';
        this.router.navigate(['contas']);
      });
    }
    else {
      this.serviceConta.excluir(this.Conta.Id.toString()).subscribe(() => {
        document.getElementById('modalExcluir')!.style.display = 'none';
        this.router.navigate(['contas']);
      });
    }
  }

  cancelar() {
    //this.router.navigate(['contas']);
    document.getElementById('modalExcluir')!.style.display = 'none';
  }

}
