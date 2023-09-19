import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { ReadDespesaDto } from 'src/app/dto/financas/despesas/readDespesaDto';
import { Categoria } from 'src/app/interfaces/categoria';
import { DespesaMapper } from 'src/app/mappers/financas/DespesaMapper';
import { DespesaService } from 'src/app/services/Financas/despesa.service';
import { CategoriaService } from 'src/app/services/categoria.service';
import { RespostaApiService } from 'src/app/services/resposta-api.service';
import { ValidacaoService } from 'src/app/services/validacao.service';

@Component({
  selector: 'app-editar-despesa',
  templateUrl: './editar-despesa.component.html',
  styleUrls: ['./editar-despesa.component.css']
})
export class EditarDespesaComponent implements OnInit {

  Despesa: ReadDespesaDto = {} as ReadDespesaDto;
  Categorias: Categoria[] = {} as Categoria[];

  constructor(
    private despesaService: DespesaService,
    private categoriaService: CategoriaService,
    private validacaoService: ValidacaoService,
    private respostaApiService: RespostaApiService,
    private despesaMapper: DespesaMapper,
    private router: Router,
    private route: ActivatedRoute
    ) { }

  ngOnInit(): void {
    const id = this.route.snapshot.paramMap.get('id');

    this.Despesa.Categoria = {
      Id: 0,
      Descricao: '',
    } as Categoria;

    this.categoriaService.listar().subscribe({
      next: (result) => {
        this.Categorias = result.valor;
      },
      error: (err) => {
        this.respostaApiService.tratarRespostaApi(err);
        this.router.navigate(['/despesas']);
      }
    });

    if (id != null) {
      this.despesaService.buscarPorId(id).subscribe({
        next: (result) => {
          this.Despesa = result.valor;
        },
        error: (err) => {
          this.respostaApiService.tratarRespostaApi(err);
          this.router.navigate(['/despesas']);
        }
      });
    }
  }

  salvarDespesa() {
    if (!this.validarCamposObrigatorios()) {
      return;
    }
    this.despesaService.atualizar(this.Despesa.Id.toString(), this.despesaMapper.ReadDtoToUpdateDto(this.Despesa)).subscribe({
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
    let camposObrigatorios = ['txtDescricao', 'txtDataDespesa', 'selCategoriaId', 'txtValorInteiro', 'txtValorCentavos', 'txtQtdParcelas'];
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
