import { DespesaService } from 'src/app/services/Financas/despesa.service';
import { Component, OnInit } from '@angular/core';
import { CategoriaService } from 'src/app/services/categoria.service';
import { ValidacaoService } from 'src/app/services/validacao.service';
import { RespostaApiService } from 'src/app/services/resposta-api.service';
import { Router } from '@angular/router';
import { Categoria } from 'src/app/interfaces/categoria';
import { CreateDespesaDto } from 'src/app/dto/financas/despesas/createDespesaDto';

@Component({
  selector: 'app-incluir-despesa',
  templateUrl: './incluir-despesa.component.html',
  styleUrls: ['./incluir-despesa.component.css']
})
export class IncluirDespesaComponent implements OnInit {

  Despesa: CreateDespesaDto = {} as CreateDespesaDto;
  Categorias: Categoria[] = {} as Categoria[];

  constructor(
    private despesaService: DespesaService,
    private categoriaService: CategoriaService,
    private validacaoService: ValidacaoService,
    private respostaApiService: RespostaApiService,
    private router: Router
    ) { }

  ngOnInit(): void {
    this.Despesa.CategoriaId = 0;
    this.Despesa.QtdParcelas = 1;
    this.categoriaService.listar().subscribe({
      next: (result) => {
        this.Categorias = result.valor;
      },
      error: (err) => {
        this.respostaApiService.tratarRespostaApi(err);
        this.router.navigate(['/despesas']);
      }
    });
  }

  salvarDespesa() {
    if (!this.validarCamposObrigatorios()) {
      return;
    }
    this.despesaService.incluir(this.Despesa).subscribe({
      next: (result) => {
        this.respostaApiService.tratarRespostaApi(result);
        this.router.navigate(['/despesas']);
      },
      error: (err) => {
        this.respostaApiService.tratarRespostaApi(err);
      }
    });
  }

  cancelar() {
    this.router.navigate(['/despesas']);
  }

  //#region Validações

  validarCamposObrigatorios(): boolean {
    let erros: number = 0;
    let camposObrigatorios = ['txtDescricao', 'txtDataDespesa', 'txtValorInteiro', 'txtValorCentavos'];
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
