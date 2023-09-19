import { Component, Input, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Cartao } from 'src/app/interfaces/financas/Cartao';
import { CartaoService } from 'src/app/services/Financas/cartao/cartao.service';
import { RespostaApiService } from 'src/app/services/resposta-api.service';

@Component({
  selector: 'app-excluir-cartao',
  templateUrl: './excluir-cartao.component.html',
  styleUrls: ['./excluir-cartao.component.css']
})
export class ExcluirCartaoComponent implements OnInit {

  @Input() Cartao: Cartao = {} as Cartao;

  constructor(
    private cartaoService: CartaoService,
    private serviceRespostaApi: RespostaApiService,
    private router: Router,
  ) { }

  ngOnInit(): void {
  }

  excluirCartao() {
    this.cartaoService.excluir(this.Cartao.Id.toString()).subscribe({
      next: (resposta) => {
        this.serviceRespostaApi.tratarRespostaApi(resposta)
        this.router.navigateByUrl('/', { skipLocationChange: true }).then(() => {
          this.router.navigate(['/cartoes']);
        });
      }, error: (err) => {
        this.serviceRespostaApi.tratarRespostaApi(err)
        document.getElementById('modalExcluirCartao')!.style.display = 'none';
      }
    });
  }

  cancelar() {
    document.getElementById('modalExcluirCartao')!.style.display = 'none';
  }

}
