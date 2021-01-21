export interface ResponseResult<TResult> {
    data?: TResult;
    totalCount?: number;
    isSuccess?: boolean;
    message?: string;
    messages: string[];
}
