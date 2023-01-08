import { Injectable } from "@angular/core";
import {HubConnection, HubConnectionState, HubConnectionBuilder} from "@microsoft/signalr";
import {CommentModel} from "../../models/CommentModel";

@Injectable({
  providedIn: 'root'
})

export class CommentService {

  hubConnection!: HubConnection;
  comments: CommentModel[] = [];

  public async startConnection(reviewId: string): Promise<void> {
    this.hubConnection = new HubConnectionBuilder()
        .withUrl('/hub-comment')
        .build();
    await this.hubConnection.start();
    this.hubConnection.serverTimeoutInMilliseconds = 600000;
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
