import { DadosFeedbackAlerta } from './interfaces/dadosFeedbackAlerta';
import { Component, OnInit } from '@angular/core';
import { FeedbackService } from './services/feedback.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  title = 'HomeTools.Front';
  DadosFeedbackAlerta: DadosFeedbackAlerta[] = [];

  constructor(
    private FeedbackService: FeedbackService
  ) { }

  ngOnInit(): void {
    this.FeedbackService.FeedbackAlertaEmitter.subscribe({
      next: (dadosFeedback: DadosFeedbackAlerta) => {
        dadosFeedback.Id = "feedback" + this.DadosFeedbackAlerta.length;
        this.DadosFeedbackAlerta.push(dadosFeedback);
      }
    });
    this.FeedbackService.FecharAletaEmitter.subscribe({
      next: (Id: string) => {
        this.DadosFeedbackAlerta = this.DadosFeedbackAlerta.filter(x=>x.Id != Id);
      }
    });
  }
}
