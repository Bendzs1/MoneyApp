import { Collection } from "typescript";

export interface User {
   id: number;
   userName: string;
   providerIds: Collection<number>;
}

export interface Authresponse {
   token: string;
   user: User;
}
