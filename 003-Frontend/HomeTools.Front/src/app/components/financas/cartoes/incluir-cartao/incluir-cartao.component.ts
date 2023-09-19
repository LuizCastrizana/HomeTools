import { CreateCartaoDto } from 'src/app/dto/financas/cartoes/createCartaoDto';
import { Cartao } from './../../../../interfaces/financas/Cartao';
import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ValidacaoService } from 'src/app/services/validacao.service';
import { CartaoService } from 'src/app/services/Financas/cartao/cartao.service';
import { RespostaApiService } from 'src/app/services/resposta-api.service';

@Component({
  selector: 'app-incluir-cartao',
  templateUrl: './incluir-cartao.component.html',
  styleUrls: ['./incluir-cartao.component.css']
})
export class IncluirCartaoComponent implements OnInit {

  Cartao: CreateCartaoDto = {} as CreateCartaoDto;

  constructor(
    private cartaoService: CartaoService,
    private validacaoService: ValidacaoService,
    private respostaApiService: RespostaApiService,
    private router: Router
  ) { }

  ngOnInit(): void {
  }

  salvarCartao() {
    if (!this.validarCamposObrigatorios(true)) {
      return;
    }
    this.cartaoService.incluir(this.Cartao).subscribe({
      next: (result) => {
        this.respostaApiService.tratarRespostaApi(result);
        this.router.navigate(['/cartoes']);
      },
      error: (err) => {
        this.respostaApiService.tratarRespostaApi(err);
      }
    });
  }

  cancelar() {
    this.router.navigate(['/cartoes']);
  }

  //#region Validações

  validarCamposObrigatorios(variavel: boolean): boolean {
    let erros: number = 0;
    let camposObrigatorios = ['txtNome', 'txtDiaVencimento', 'txtDiaFechamento'];
    camposObrigatorios.forEach(campo => {
      if (!this.campoObrigatorio(campo)) {
        erros++;
      }
    });
    return erros == 0 ? true : false;
  }

  campoObrigatorio(campoId: string): boolean {
    let campo = <HTMLInputElement>document.getElementById(campoId)!;
    let campoErro = document.getElementById('erro_' + campoId)!;
    if (campo.value === "" || campo.value == null || campo.value == undefined || (campo.tagName == 'SELECT' && campo.value == '0')) {
      campo.classList.add('campo-obrigatorio');
      campoErro.style.display = 'block';
      campoErro.innerHTML = "Campo obrigatório";
      return false;
    }
    else {
      campoErro.style.display = 'none';
      campo.classList.remove('campo-obrigatorio');
      return true;
    }
  }

  apenasNumeros(event: any) {
    if (!this.validacaoService.apenasNumeros(event)) {
      event.preventDefault();
    }
  }

  //#endregion
}
