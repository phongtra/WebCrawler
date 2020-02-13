import React, { useState, useEffect } from "react";
import { Link } from "react-router-dom";
import Axios from "axios";
import { Card, Image, Container } from "semantic-ui-react";

const ComicList = () => {
  const [comics, setComics] = useState([]);
  useEffect(() => {
    const fetchComics = async () => {
      const res = await Axios.get("http://localhost:5000/api/values");
      setComics(res.data);
    };
    fetchComics();
  }, []);
  console.log(comics);
  return (
    <Container>
      <Card.Group itemsPerRow={6} centered>
        {comics.map((comic, i) => {
          return (
            <Card key={i}>
              <Link to={`/${comic.titleNo}`}>
                <Image src={comic.imageLink} size="small" centered />
              </Link>
              <Card.Content>
                <Card.Header centered>{comic.subject}</Card.Header>
                <Link style={{ color: "black" }} to={`/${comic.titleNo}`}>
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
          );
        })}
      </Card.Group>
    </Container>
  );
};

export default ComicList;
