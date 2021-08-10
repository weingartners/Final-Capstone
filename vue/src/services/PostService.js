import axios from 'axios';

const http = axios.create({
  baseURL: "https://localhost:44315"
});

export default {

    getPost(forumId) {
        return axios.get(`/forum/${forumId}`)
      },

    getSinglePost(postId){
      return axios.get(`/posts/${postId}`)
    },

    addPost(post) {
      return axios.post('/forum', post)
    },

    getReplies(postId) {
      return axios.get(`/post/${postId}`)
    },

    addReply(reply) {
      return axios.post('/post', reply)
    },

    updateUpvote(postId) {
      return axios.put(`/upvotes${postId}`) //takes postId, all other logic handled in back end
    },

    updateDownvote(postId) {
      return axios.put(`/downvotes${postId}`)
    },

    getForum() {
      return axios.get('/')
    }
}
