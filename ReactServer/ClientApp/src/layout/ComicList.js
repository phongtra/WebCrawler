import './ComicList.css';
import React, { useState, useEffect } from 'react';
import { Link } from 'react-router-dom';
import Axios from 'axios';
import { Card, Image, Grid } from 'semantic-ui-react';

const ComicList = () => {
  const [comics, setComics] = useState([]);

  useEffect(() => {
    const fetchComics = async () => {
      const res = await Axios.get('/values');
      setComics(res.data);
    };
    fetchComics();
  }, []);

  return (
    <Grid doubling columns={5} centered>
      {comics.map((comic, i) => {
        return (
          <Grid.Column key={i}>
            <Card>
              <Link to={`/${comic.titleNo}`}>
                <Image src={`/comic${comic.imageLink}`} size="small" centered />
              </Link>
              <Card.Content>
                <Card.Header centered>{comic.subject}</Card.Header>
                <Link style={{ color: 'black' }} to={`/${comic.titleNo}`}>
                  <Card.Description>
                    <b>Genre: </b>
                    {comic.genre}
                  </Card.Description>
                  <Card.Description>
                    <b>Author: </b>
                    {comic.author}
                  </Card.Description>
                </Link>
              </Card.Content>
            </Card>
          </Grid.Column>
        );
      })}
    </Grid>
  );
};

export default ComicList;
