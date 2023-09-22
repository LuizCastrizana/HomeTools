import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { Router } from '@angular/router';
import { Categoria } from 'src/app/interfaces/categoria';
import { Compra } from 'src/app/interfaces/financas/Compra';
import { CategoriaService } from 'src/app/services/categoria.service';
import { RespostaApiService } from 'src/app/services/resposta-api.service';
import { ValidacaoService } from 'src/app/services/validacao.service';

@Component({
  selector: 'app-modal-formulario-compra',
  templateUrl: './modal-formulario.component.html',
  styleUrls: ['./modal-formulario.component.css'],
})
export class ModalFormularioCompraComponent implements OnInit {
  @Input() Compra: Compra = {} as Compra;
  @Input() Edicao: boolean = false;
  @Input() CartaoId: number = 0;
  @Output() CompraChange = new EventEmitter<Compra>();
  @Output() EventoEdicao = new EventEmitter<boolean>();

  CategoriaId: number = 0;
  Categorias: Categoria[] = [];

  constructor(
    private validacaoService: ValidacaoService,
    private categoriaService: CategoriaService,
    private respostaApiService: RespostaApiService,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.categoriaService.listar().subscribe({
      next: (result) => {
        this.Categorias = result.valor;
      },
      error: (err) => {
        this.respostaApiService.tratarRespostaApi(err);
        this.router.navigate(['/cartoes']);
      },
    });
  }

  ngOnChanges(): void {
    if (this.Compra.Id > 0) {
      this.CategoriaId = this.Compra.Categoria.Id;
    } else {
      this.CategoriaId = 0;
    }
  }

  salvarDados() {
    if (this.validarCamposObrigatorios()) {
      this.Compra.Categoria = {
        Id: this.CategoriaId,
        Descricao: '',
      } as Categoria;
      this.Compra.CartaoId = this.CartaoId;
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
      'txtDescricaoCompra',
      'txtDataCompra',
      'txtValorInteiroCompra',
      'txtValorCentavosCompra',
      'txtQtdParcelasCompra',
      'selCategoriaIdCompra',
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
