import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { CreateAssinaturaDto } from 'src/app/dto/financas/cartoes/createAssinaturaDto';
import { ReadAssinaturaDto } from 'src/app/dto/financas/cartoes/readAssinaturaDto';
import { RespostaApi } from 'src/app/dto/respostaApi';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class AssinaturaService {

  constructor(private http: HttpClient) { }

  private readonly  API = environment.laPlataApiAdress + '/api/Assinatura';

  listar(): Observable<RespostaApi<ReadAssinaturaDto[]>> {
    return this.http.get<RespostaApi<ReadAssinaturaDto[]>>(this.API);
  }

  buscar(busca: string): Observable<RespostaApi<ReadAssinaturaDto[]>> {
    const url = `${this.API}/?busca=${busca}`
    return this.http.get<RespostaApi<ReadAssinaturaDto[]>>(url)
  }

  buscarPorId(id: string): Observable<RespostaApi<ReadAssinaturaDto>> {
    const url = `${this.API}/${id}`
    return this.http.get<RespostaApi<ReadAssinaturaDto>>(url)
  }

  buscarPorCartaoId(id: string): Observable<RespostaApi<ReadAssinaturaDto[]>> {
    const url = `${this.API}/ObterPorCartaoId/${id}`
    return this.http.get<RespostaApi<ReadAssinaturaDto[]>>(url)
  }

  incluir (CreateContaDto: CreateAssinaturaDto): Observable<RespostaApi<ReadAssinaturaDto>> {
    return this.http.post<RespostaApi<ReadAssinaturaDto>>(this.API, CreateContaDto)
  }

  atualizar (id: string, UpdateContaDto: CreateAssinaturaDto): Observable<RespostaApi<ReadAssinaturaDto>> {
    const url = `${this.API}/${id}`
    return this.http.put<RespostaApi<ReadAssinaturaDto>>(url, UpdateContaDto)
  }

  excluir (id: string): Observable<RespostaApi<ReadAssinaturaDto>> {
    const url = `${this.API}/${id}`
    return this.http.delete<RespostaApi<ReadAssinaturaDto>>(url)
  }
}

