

export interface User {
    id: number;
    userName: string;
    personFirstName: string;
    personLastName: string;
    password: string;
    lastLoginDate?: Date;
    isEnabled: boolean;
}

export interface UserProfileViewModel {
    userName: string;
    oldPassword: string;
    newPassword: string;
    repeatPassword: string;
}