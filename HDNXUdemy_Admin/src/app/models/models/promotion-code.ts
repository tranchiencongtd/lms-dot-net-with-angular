export class PromotionCodeModel {
    id?: number;
    idCoupon?: string;
    stripePromotionCodeId?: string;
    object?: string;
    customer?: string;
    expiresAt?: Date;
    livemode?: boolean;
    maxRedemptions: number;
    timesRedeemed: number;
    valueOff?: number;
    createDate?: Date;
    status?: number;
}