import { CreateContaDto } from 'src/app/dto/financas/contas/createContaDto';
import { UpdateContaDto } from 'src/app/dto/financas/contas/updateContaDto';
import { RespostaApi } from '../../dto/respostaApi';
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { ReadContaDto } from '../../dto/financas/contas/readContaDto';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ContaService {

  constructor(private http: HttpClient) { }

  private readonly API = "https://localhost:7012/api/Conta";

  listar(): Observable<RespostaApi<ReadContaDto[]>> {
    return this.http.get<RespostaApi<ReadContaDto[]>>(this.API);
  }

  buscar(busca: string): Observable<RespostaApi<ReadContaDto[]>> {
    const url = `${this.API}/?busca=${busca}`
    return this.http.get<RespostaApi<ReadContaDto[]>>(url)
  }

  buscarPorId(id: string): Observable<RespostaApi<ReadContaDto>> {
    const url = `${this.API}/${id}`
    return this.http.get<RespostaApi<ReadContaDto>>(url)
  }

  incluir (CreateContaDto: CreateContaDto): Observable<RespostaApi<ReadContaDto>> {
    return this.http.post<RespostaApi<ReadContaDto>>(this.API, CreateContaDto)
  }

  atualizar (id: string, UpdateContaDto: UpdateContaDto): Observable<RespostaApi<ReadContaDto>> {
    const url = `${this.API}/${id}`
    return this.http.put<RespostaApi<ReadContaDto>>(url, UpdateContaDto)
  }

  excluir (id: string): Observable<RespostaApi<ReadContaDto>> {
    const url = `${this.API}/${id}`
    return this.http.delete<RespostaApi<ReadContaDto>>(url)
  }
}
