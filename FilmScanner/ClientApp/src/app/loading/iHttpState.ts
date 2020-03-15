import { HttpProgressState } from './httpProgressState';

export interface IHttpState {
    url: string;
    state: HttpProgressState;
}
