import { DadosModalExcluir } from './../../interfaces/dadosModalExcluir';
import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';

@Component({
  selector: 'app-modal-excluir',
  templateUrl: './modal-excluir.component.html',
  styleUrls: ['./modal-excluir.component.css']
})
export class ModalExcluirComponent implements OnInit {

  @Input() DadosModalExcluir: DadosModalExcluir = {} as DadosModalExcluir;
  @Output() ExcluirRegistro = new EventEmitter<DadosModalExcluir>();

  constructor() { }

  ngOnInit(): void {
  }

  excluirRegistro() {
    this.ExcluirRegistro.emit(this.DadosModalExcluir);
  }

  cancelar() {
    document.getElementById('modalExcluir')!.style.display = 'none';
  }

}
