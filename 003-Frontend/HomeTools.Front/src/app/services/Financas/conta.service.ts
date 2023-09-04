import { RespostaApi } from './../../interfaces/api-dto/respostaApi';
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { ReadContaDto } from '../../interfaces/api-dto/Financas/readConta';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ContaService {

  constructor(private http: HttpClient) { }

  private readonly ContaAPI = "https://localhost:7012/api/Conta";

  listar(): Observable<RespostaApi<ReadContaDto[]>> {
    return this.http.get<RespostaApi<ReadContaDto[]>>(this.ContaAPI);
  }

  buscar(busca: string): Observable<RespostaApi<ReadContaDto[]>> {
    const url = `${this.ContaAPI}/${busca}`
    return this.http.get<RespostaApi<ReadContaDto[]>>(url)
  }
}
