import type { UserResponseContract } from "./UserResponseContract"

export interface CommentResponseContract {
commentId: Number
poster?: UserResponseContract
postTime:string
sceneID:Number
commentText: string
}