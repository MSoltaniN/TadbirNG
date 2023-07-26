import { FullAccount } from "@sppc/finance/models";

export class PayReceiveCashAccount {
    id: number;
    payReceiveId: number;
    amount: number;
    remarks: string;
    fullAccount: FullAccount;
    sourceAppId: number;
    isBank: boolean = true;
    bankOrderNo: number;
}
