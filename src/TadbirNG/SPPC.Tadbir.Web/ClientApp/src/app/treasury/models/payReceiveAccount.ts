import { FullAccount } from "@sppc/finance/models";

export interface PayReceiveAccount {
    id: number,
    payReceiveId: number,
    amount: number,
    remarks: string,
    fullAccount: FullAccount
} 