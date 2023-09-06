import { TipoAlertaEnum } from "../enums/tipoAlertaEnum";

export interface DadosFeedbackAlerta {
    Id: string;
    TipoAlerta: TipoAlertaEnum;
    Titulo: string;
    Mensagem: string;
}
