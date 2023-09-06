import { Categoria } from "../../categoria";
import { PagamentoContaDto } from "./readPagamentoContaDto";

export interface ReadContaDto {
  Id: number;
  Descricao: string;
  ValorInteiro: number;
  ValorCentavos: number;
  DiaVencimento: number;
  Categoria: Categoria;
  Pagamentos: PagamentoContaDto[];
}
