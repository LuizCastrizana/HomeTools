import { CreatePagamentoContaVariavelDto } from '../../dto/financas/contas/createPagamentoContaVariavelDto';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { PagamentoContaVariavelDto } from 'src/app/dto/financas/contas/readPagamentoContaVariavelDto';
import { RespostaApi } from 'src/app/dto/respostaApi';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class PagamentoContaVariavelService {

  constructor(private http: HttpClient) { }

  private readonly API = environment.laPlataApiAdress + "/api/PagamentoContaVariavel";

  create(CreatePagamentoContaVariavelDto: CreatePagamentoContaVariavelDto) {
    return this.http.post<RespostaApi<PagamentoContaVariavelDto>>(this.API, CreatePagamentoContaVariavelDto);
  }

}
