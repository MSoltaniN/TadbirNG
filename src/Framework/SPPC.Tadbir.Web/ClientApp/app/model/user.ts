

export interface User {
    id: number;
    userName: string;
    personFirstName: string;
    personLastName: string;
    passwordHash: string;
    lastLoginDate?: Date;
    isEnabled: boolean;
}