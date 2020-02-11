// import './ArticleDetail.css';
import React, { useState, useEffect } from "react";
import { Link } from "react-router-dom";
import Axios from "axios";
import { Card, Image, Container } from "semantic-ui-react";

const ComicDetail = props => {
  const [episodes, setEpisodes] = useState([]);
  useEffect(() => {
    const fetchEpisodes = async () => {
      const res = await Axios.get(
        `http://localhost:5000/api/values/${props.match.params.titleNo}`
      );
      setEpisodes(res.data);
    };
    fetchEpisodes();
  }, []);
  console.log(episodes);
  if (episodes.length === 0) return <div>Coming soon</div>;
  return (
    <Container>
      <Card.Group>
        {episodes.map((episode, i) => {
          return (
            <Card key={i}>
              <Card.Content>
                <Link to={`/${episode.titleNo}/${episode.episodeLinkHash}`}>
                  <Image
                    floated="left"
                    size="small"
                    src={episode.episodeThumbnail}
                  />
                </Link>
                <Card.Header
                  dangerouslySetInnerHTML={{ __html: episode.episodeName }}
                ></Card.Header>
                <Link
                  style={{ color: "black" }}
                  to={`/${episode.titleNo}/${episode.episodeLinkHash}`}
                >
                  <Card.Description>{episode.episodeDate}</Card.Description>
                </Link>
              </Card.Content>
            </Card>
          );
        })}
      </Card.Group>
    </Container>
  );
};

export default ComicDetail;
