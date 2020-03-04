import React from "react";
import { Link } from "react-router-dom";
import { Menu, Input, Responsive, Dropdown } from "semantic-ui-react";

const NavBar = props => {
  const genres = [
    "Drama",
    "Fantasy",
    "Comedy",
    "Action",
    "Slice of life",
    "Romance",
    "Superhero",
    "Historical",
    "Thriller",
    "Sports"
  ];
  const handleFilter = e => {
    props.handleFilter(e.target.value);
  };
  return (
    <>
      <Menu>
        <Link to="/" className="header item">
          WebToon Clone
        </Link>
        <Dropdown item text={props.genre ?? "Comics by Genres"}>
          <Dropdown.Menu>
            <a className="item" role="option" href="/">
              All Genres
            </a>
            {genres.map((genre, i) => {
              return (
                <a href={`/${genre}`} key={i} className="item" role="option">
                  {genre}
                </a>
              );
            })}
          </Dropdown.Menu>
        </Dropdown>
        {props.placeholder && props.handleFilter && (
          <Menu.Menu position="right">
            <Responsive minWidth={600}>
              <Menu.Item>
                <Input
                  onChange={handleFilter}
                  icon="search"
                  placeholder={props.placeholder}
                />
              </Menu.Item>
            </Responsive>
          </Menu.Menu>
        )}

        {/* <Menu.Item name="upcomingEvents">Upcoming Events</Menu.Item> */}
      </Menu>
      {props.placeholder && props.handleFilter && (
        <Responsive maxWidth={500}>
          <Input
            fluid
            icon="search"
            placeholder={props.placeholder}
            onChange={handleFilter}
          />
        </Responsive>
      )}
    </>
  );
};

export default NavBar;
