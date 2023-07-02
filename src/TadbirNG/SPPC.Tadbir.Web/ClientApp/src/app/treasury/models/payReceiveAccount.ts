import { FullAccount } from "@sppc/finance/models";

export interface PayReceiveAccount {
    id: number,
    payReceiveId: number,
    amount: number,
    description: string,
    fullAccount: FullAccount
} 