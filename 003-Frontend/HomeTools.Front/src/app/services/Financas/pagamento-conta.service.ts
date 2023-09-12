import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { CreatePagamentoContaDto } from 'src/app/dto/financas/contas/createPagamentoContaDto';
import { PagamentoContaDto } from 'src/app/dto/financas/contas/readPagamentoContaDto';
import { RespostaApi } from 'src/app/dto/respostaApi';

@Injectable({
  providedIn: 'root'
})
export class PagamentoContaService {

  constructor(private http: HttpClient) { }

  private readonly API = "https://localhost:7012/api/PagamentoConta";

  create(CreatePagamentoContaDto: CreatePagamentoContaDto) {
    return this.http.post<RespostaApi<PagamentoContaDto>>(this.API, CreatePagamentoContaDto);
  }
}
