export interface DetailedReviewModel {
  title: string;
  artName: string;
  category: string;
  description: string;
  tags: string[];
  imagesUrl?: string[];
  authorName: string;
  userRating: number;
  averageRating: number;
  likesCount: number;
  likeStatus: boolean;
  authorGrade: number;
  creationDate: Date;
}
