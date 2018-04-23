

export interface User {
    id: number;
    userName: string;
    personFirstName: string;
    personLastName: string;
    password: string;
    lastLoginDate?: Date;
    isEnabled: boolean;
}