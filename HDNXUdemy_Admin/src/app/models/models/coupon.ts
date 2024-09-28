export class CouponModel {
    nameOfCoupon: string;
    id: number;
    stripeCouponId: string;
    object: string;
    amountOff: string;
    currency: string;
    duration: string;
    durationInMonths: number;
    livemode: boolean;
    maxRedemptions: number;
    metadata: string;
    name: string;
    percentOff: number;
    redeemBy: number;
    timesRedeemed: number;
    valid: boolean;
    created?: Date;
    startDate?: Date;
    endDate?: Date;
    status?: number;
}
