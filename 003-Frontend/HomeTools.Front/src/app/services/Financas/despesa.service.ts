import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { CreateDespesaDto } from 'src/app/dto/financas/despesas/createDespesaDto';
import { UpdateDespesaDto } from 'src/app/dto/financas/despesas/updateDespesaDto';
import { ReadDespesaDto } from 'src/app/dto/financas/despesas/readDespesaDto';
import { RespostaApi } from 'src/app/dto/respostaApi';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class DespesaService {

  constructor(private http: HttpClient) { }

  private readonly API = environment.laPlataApiAdress + "/api/Despesa";

  listar(): Observable<RespostaApi<ReadDespesaDto[]>> {
    return this.http.get<RespostaApi<ReadDespesaDto[]>>(this.API);
  }

  buscar(busca: string): Observable<RespostaApi<ReadDespesaDto[]>> {
    const url = `${this.API}/?busca=${busca}`
    return this.http.get<RespostaApi<ReadDespesaDto[]>>(url);
  }

  buscarPorId(id: string): Observable<RespostaApi<ReadDespesaDto>> {
    const url = `${this.API}/${id}`
    return this.http.get<RespostaApi<ReadDespesaDto>>(url)
  }

  incluir (CreateDespesaDto: CreateDespesaDto): Observable<RespostaApi<ReadDespesaDto>> {
    return this.http.post<RespostaApi<ReadDespesaDto>>(this.API, CreateDespesaDto)
  }

  atualizar (id: string, UpdateDespesaDto: UpdateDespesaDto): Observable<RespostaApi<ReadDespesaDto>> {
    const url = `${this.API}/${id}`
    return this.http.put<RespostaApi<ReadDespesaDto>>(url, UpdateDespesaDto)
  }

  excluir (id: string): Observable<RespostaApi<ReadDespesaDto>> {
    const url = `${this.API}/${id}`
    return this.http.delete<RespostaApi<ReadDespesaDto>>(url)
  }
}
