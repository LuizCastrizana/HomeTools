import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { RespostaApi } from 'src/app/dto/respostaApi';
import { environment } from 'src/environments/environment';
import { ReadCompraDto } from 'src/app/dto/financas/cartoes/readCompraDto';
import { CreateCompraDto } from 'src/app/dto/financas/cartoes/createCompraDto';

@Injectable({
  providedIn: 'root'
})
export class CompraService {

  constructor(private http: HttpClient) { }

  private readonly  API = environment.laPlataApiAdress + '/api/Compra';

  listar(): Observable<RespostaApi<ReadCompraDto[]>> {
    return this.http.get<RespostaApi<ReadCompraDto[]>>(this.API);
  }

  buscar(busca: string): Observable<RespostaApi<ReadCompraDto[]>> {
    const url = `${this.API}/?busca=${busca}`
    return this.http.get<RespostaApi<ReadCompraDto[]>>(url)
  }

  buscarPorId(id: string): Observable<RespostaApi<ReadCompraDto>> {
    const url = `${this.API}/${id}`
    return this.http.get<RespostaApi<ReadCompraDto>>(url)
  }

  buscarPorCartaoId(id: string): Observable<RespostaApi<ReadCompraDto[]>> {
    const url = `${this.API}/ObterPorCartaoId/${id}`
    return this.http.get<RespostaApi<ReadCompraDto[]>>(url)
  }

  incluir (CreateContaDto: CreateCompraDto): Observable<RespostaApi<ReadCompraDto>> {
    return this.http.post<RespostaApi<ReadCompraDto>>(this.API, CreateContaDto)
  }

  atualizar (id: string, UpdateContaDto: CreateCompraDto): Observable<RespostaApi<ReadCompraDto>> {
    const url = `${this.API}/${id}`
    return this.http.put<RespostaApi<ReadCompraDto>>(url, UpdateContaDto)
  }

  excluir (id: string): Observable<RespostaApi<ReadCompraDto>> {
    const url = `${this.API}/${id}`
    return this.http.delete<RespostaApi<ReadCompraDto>>(url)
  }
}
