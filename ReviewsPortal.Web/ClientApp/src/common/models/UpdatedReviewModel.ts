export interface UpdatedReviewModel {
  title: string
  artName: string
  categoryName: string
  description: string
  tags: string[]
  images?: ImageData[]
  authorName: string
  grade: number
}
