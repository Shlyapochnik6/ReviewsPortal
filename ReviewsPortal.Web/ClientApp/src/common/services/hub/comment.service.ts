import { Injectable } from "@angular/core";
import * as signalR from "@microsoft/signalr"
import {CommentModel} from "../../models/CommentModel";

@Injectable({
  providedIn: 'root'
})

export class CommentService {

  comments: CommentModel[] = [];

  private hubConnection = new signalR.HubConnectionBuilder()
    .withUrl('/hub-comment')
    .build();

  async startConnection(reviewId: string) {
    await this.hubConnection.start();
    await this.hubConnection.invoke("ConnectToGroup", reviewId);
  }

  async sendComment(reviewId: string, commentId: string) {
    await this.hubConnection.invoke("SendComment", reviewId, commentId);
  }

  async getComment() {
    await this.hubConnection.on("GetComment", (comment: CommentModel) => {
      this.comments.push(comment);
    })
  }
}
