import { Component, Input, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ReadConta } from 'src/app/interfaces/financas/readConta';
import { ContaVariavelService } from 'src/app/services/Financas/conta-variavel.service';
import { ContaService } from 'src/app/services/Financas/conta.service';
import { RespostaApiService } from 'src/app/services/resposta-api.service';

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
    private serviceRespostaApi: RespostaApiService,
    private router: Router,
    ) { }

  ngOnInit(): void {
  }

  excluirConta() {
    if (this.Conta.Variavel) {
      this.serviceContaVariavel.excluir(this.Conta.Id.toString()).subscribe({
        next: (resposta) => {
          this.serviceRespostaApi.tratarRespostaApi(resposta)
          this.router.navigateByUrl('/', { skipLocationChange: true }).then(() => {
            this.router.navigate(['/contas']);
          });
        }, error: (err) => {
          this.serviceRespostaApi.tratarRespostaApi(err)
          document.getElementById('modalExcluir')!.style.display = 'none';
        }
      });
    }
    else {
      this.serviceConta.excluir(this.Conta.Id.toString()).subscribe({
        next: (resposta) => {
          this.serviceRespostaApi.tratarRespostaApi(resposta)
          this.router.navigateByUrl('/', { skipLocationChange: true }).then(() => {
            this.router.navigate(['/contas']);
          });
        }, error: (err) => {
          this.serviceRespostaApi.tratarRespostaApi(err)
          document.getElementById('modalExcluir')!.style.display = 'none';
        }
      });
    }
  }

  cancelar() {
    document.getElementById('modalExcluir')!.style.display = 'none';
  }

}
