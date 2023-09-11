import { CreatePagamentoContaVariavelDto } from './../../interfaces/api-dto/financas/createPagamentoContaVariavelDto';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { PagamentoContaVariavelDto } from 'src/app/interfaces/api-dto/financas/readPagamentoContaVariavelDto';
import { RespostaApi } from 'src/app/interfaces/api-dto/respostaApi';

@Injectable({
  providedIn: 'root'
})
export class PagamentoContaVariavelService {

  constructor(private http: HttpClient) { }

  private readonly API = "https://localhost:7012/api/PagamentoContaVariavel";

  create(CreatePagamentoContaVariavelDto: CreatePagamentoContaVariavelDto) {
    return this.http.post<RespostaApi<PagamentoContaVariavelDto>>(this.API, CreatePagamentoContaVariavelDto);
  }

}
