import React, { useState, useEffect } from "react";
import Loading from "../commons/Loading";
import AdGrid from "../commons/AdGrid";
import NavBar from "../nav/NavBar";
import { Grid, Card, Image } from "semantic-ui-react";
import { Link } from "react-router-dom";
import Axios from "axios";

const ComicGenreSort = props => {
  const [comics, setComics] = useState([]);
  const [loading, setLoading] = useState(true);
  const [noContent, setNoContent] = useState("");
  const [filter, setFilter] = useState([]);
  const { genre } = props.match.params;
  useEffect(() => {
    const fetchComics = async () => {
      try {
        const res = await Axios.get("/values");
        const comicSort = [...res.data].filter(comic => comic.genre === genre);
        setComics(comicSort);
        setFilter(comicSort);
      } catch {
        setNoContent("oops, something went wrong, please come back later");
        setLoading(false);
      }
    };
    fetchComics();
    setLoading(false);
  }, []);
  const handleFilter = filter => {
    setFilter(
      [...comics].filter(
        comic => comic.subject.toLowerCase().indexOf(filter.toLowerCase()) > -1
      )
    );
  };
  if (loading) {
    return <Loading />;
  }
  if (noContent) {
    return (
      <AdGrid>
        <div>{noContent}</div>
      </AdGrid>
    );
  }

  return (
    <>
      <NavBar
        genre={genre}
        placeholder={`Search from ${genre}`}
        filter={comics}
        handleFilter={handleFilter}
      />
      <AdGrid>
        <Grid doubling columns={4} centered>
          {filter.map((comic, i) => {
            return (
              <Grid.Column key={i}>
                <Card>
                  <Link
                    to={`/${comic.genre}/${comic.subject}/${comic.titleNo}`}
                  >
                    <Image
                      src={`/comic${comic.imageLink}`}
                      size="small"
                      centered
                    />
                  </Link>
                  <Card.Content>
                    <Card.Header centered="true">{comic.subject}</Card.Header>
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
              </Grid.Column>
            );
          })}
        </Grid>
      </AdGrid>
    </>
  );
};

export default ComicGenreSort;
