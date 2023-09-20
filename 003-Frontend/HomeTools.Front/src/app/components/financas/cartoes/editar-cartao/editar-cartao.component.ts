import { Cartao } from 'src/app/interfaces/financas/Cartao';
import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { CartaoMapper } from 'src/app/mappers/financas/CartaoMapper';
import { CartaoService } from 'src/app/services/Financas/cartao/cartao.service';
import { RespostaApiService } from 'src/app/services/resposta-api.service';
import { ValidacaoService } from 'src/app/services/validacao.service';

@Component({
  selector: 'app-editar-cartao',
  templateUrl: './editar-cartao.component.html',
  styleUrls: ['./editar-cartao.component.css']
})
export class EditarCartaoComponent implements OnInit {

  Cartao: Cartao = {} as Cartao;
  Id: string | null = "";

  constructor(
    private cartaoService: CartaoService,
    private validacaoService: ValidacaoService,
    private respostaApiService: RespostaApiService,
    private router: Router,
    private ActivatedRoute: ActivatedRoute
  ) { }

  ngOnInit(): void {
    this.Id = this.ActivatedRoute.snapshot.paramMap.get('id');
    this.cartaoService.buscarPorId(this.Id!).subscribe({
      next: (respostaApi) => {
      this.Cartao = CartaoMapper.CartaoDtoToCartao(respostaApi.valor);
      },
      error: (err) => {
        this.respostaApiService.tratarRespostaApi(err);
        this.router.navigate(['/cartoes']);
      }
    });
  }

  salvarCartao() {
    if (!this.validarCamposObrigatorios(true)) {
      return;
    }
    let CreateCartaoDto = CartaoMapper.CartaoToCreateCartaoDto(this.Cartao);
    this.cartaoService.atualizar(this.Id!, CreateCartaoDto).subscribe({
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
