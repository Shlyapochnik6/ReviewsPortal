export interface BriefReviewModel {
  reviewId: string;
  title: string;
  artName: string;
  authorGrade: number;
  creationDate: Date;
  averageRating: number;
  likesCount: number;
  tags: string[];
}
