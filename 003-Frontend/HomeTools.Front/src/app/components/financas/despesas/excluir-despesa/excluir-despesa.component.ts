import { Component, Input, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Despesa } from 'src/app/interfaces/financas/Despesa';
import { DespesaService } from 'src/app/services/Financas/despesa.service';
import { RespostaApiService } from 'src/app/services/resposta-api.service';

@Component({
  selector: 'app-excluir-despesa',
  templateUrl: './excluir-despesa.component.html',
  styleUrls: ['./excluir-despesa.component.css']
})
export class ExcluirDespesaComponent implements OnInit {

  @Input() Despesa: Despesa = {} as Despesa;

  constructor(
    private serviceDespesa: DespesaService,
    private serviceRespostaApi: RespostaApiService,
    private router: Router,
  ) { }

  ngOnInit(): void {
  }

  excluirDespesa() {

  }

  cancelar() {
    document.getElementById('modalExcluir')!.style.display = 'none';
  }

}
