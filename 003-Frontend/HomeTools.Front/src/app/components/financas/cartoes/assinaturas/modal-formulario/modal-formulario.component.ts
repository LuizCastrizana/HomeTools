import { Assinatura } from './../../../../../interfaces/financas/Assinatura';
import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { Router } from '@angular/router';
import { CategoriaService } from 'src/app/services/categoria.service';
import { RespostaApiService } from 'src/app/services/resposta-api.service';
import { ValidacaoService } from 'src/app/services/validacao.service';

@Component({
  selector: 'app-modal-formulario-assinatura',
  templateUrl: './modal-formulario.component.html',
  styleUrls: ['./modal-formulario.component.css'],
})
export class ModalFormularioAssinaturaComponent implements OnInit {

  @Input() Assinatura: Assinatura = {} as Assinatura;
  @Input() Edicao: boolean = false;
  @Input() CartaoId: number = 0;
  @Output() AssinaturaChange = new EventEmitter<Assinatura>();
  @Output() EventoEdicao = new EventEmitter<boolean>();

  constructor(
    private validacaoService: ValidacaoService,
    private router: Router
  ) {}

  ngOnInit(): void {}

  salvarDados() {
    if (this.validarCamposObrigatorios()) {
      this.Assinatura.CartaoId = this.CartaoId;
      this.EventoEdicao.emit(this.Edicao);
    }
  }

  fechar() {
    this.router.navigateByUrl('/', { skipLocationChange: true }).then(() => {
      this.router.navigate(['/cartoes/editar-cartao/' + this.CartaoId]);
    });
  }

  //#region Validações

  validarCamposObrigatorios(): boolean {
    let erros: number = 0;
    let camposObrigatorios = [
      'txtDescricaoAssinatura',
      'txtDiaVencimentoAssinatura',
      'txtValorInteiroAssinatura',
      'txtValorCentavosAssinatura',
    ];
    camposObrigatorios.forEach((campo) => {
      if (!this.campoObrigatorio(campo)) {
        erros++;
      }
    });
    return erros == 0 ? true : false;
  }

  campoObrigatorio(campoId: string): boolean {
    let campo = <HTMLInputElement>document.getElementById(campoId)!;
    let campoErro = document.getElementById('erro_' + campoId)!;
    if (
      campo.value === '' ||
      campo.value == null ||
      campo.value == undefined ||
      (campo.tagName == 'SELECT' && campo.value == '0')
    ) {
      campo.classList.add('campo-obrigatorio');
      campoErro.style.display = 'block';
      campoErro.innerHTML = 'Campo obrigatório';
      return false;
    } else {
      campoErro.style.display = 'none';
      campo.classList.remove('campo-obrigatorio');
      return true;
    }
  }

  apenasNumerosPositivos(event: any) {
    if (!this.validacaoService.apenasNumerosPositivos(event)) {
      event.preventDefault();
    }
  }

  apenasNumeros(event: any) {
    if (!this.validacaoService.apenasNumeros(event)) {
      event.preventDefault();
    }
  }

  //#endregion
}
