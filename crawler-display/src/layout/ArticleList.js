import React, { useState, useEffect } from 'react';
import { Link } from 'react-router-dom';
import Axios from 'axios';
import { Card, Image, Container } from 'semantic-ui-react';

const ArticleList = () => {
  const [articles, setArticles] = useState([]);
  useEffect(() => {
    const fetchArticles = async () => {
      const res = await Axios.get('http://localhost:5000/api/values');
      setArticles(res.data);
    };
    fetchArticles();
  }, []);
  return (
    <Container>
      <Card.Group>
        {articles.map((article, i) => {
          return (
            <Card key={i}>
              <Card.Content>
                <Image floated="left" size="mini" src={article.imageLink} />
                <Card.Header>{article.title}</Card.Header>
                <Link style={{ color: 'black' }} to={`/${article.id}`}>
                  <Card.Description
                    dangerouslySetInnerHTML={{ __html: article.description }}
                  />
                </Link>
              </Card.Content>
            </Card>
          );
        })}
      </Card.Group>
    </Container>
  );
};

export default ArticleList;
