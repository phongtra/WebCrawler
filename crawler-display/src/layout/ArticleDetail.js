import './ArticleDetail.css';
import React, { useState, useEffect } from 'react';
import Axios from 'axios';

const ArticleDetail = props => {
  const [article, setArticle] = useState({});
  useEffect(() => {
    const fetchArticle = async () => {
      const res = await Axios.get(
        `http://localhost:5000/api/values/${props.match.params.id}`
      );
      setArticle(res.data);
    };
    fetchArticle();
  }, []);
  console.log(article);
  if (!article) return <div>Loading..</div>;
  return <div dangerouslySetInnerHTML={{ __html: article.content }} />;
};

export default ArticleDetail;
