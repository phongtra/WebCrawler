import React, { useState, useEffect } from "react";
import Axios from "axios";
import { Container } from "semantic-ui-react";
// var request;
// if (window.XMLHttpRequest) {
//   // Mozilla, Safari, ...
//   request = new XMLHttpRequest(); }
//   else if (window.ActiveXObject) { // IE
//     try { request = new ActiveXObject('Msxml2.XMLHTTP'); }
//      catch (e) { try { request = new ActiveXObject('Microsoft.XMLHTTP'); }
//      catch (e) {} } } request.open("GET", url, true); request.send();
const EpisodeDetail = props => {
  const [episodeContent, setEpisodeContent] = useState({});
  useEffect(() => {
    const fetchEpisodeContent = async () => {
      const res = await Axios.get(
        `http://localhost:5000/api/values/${props.match.params.titleNo}/${props.match.params.hash}`
      );
      setEpisodeContent(res.data);
    };
    fetchEpisodeContent();
  }, []);
  if (episodeContent.content) {
    JSON.parse(episodeContent.content).forEach(img => {
      var request;
      if (window.XMLHttpRequest) {
        // Mozilla, Safari, ...
        request = new XMLHttpRequest();
        
      }
      // } else if (window.ActiveXObject) {
      //   // IE
      //   try {
      //     request = new ActiveXObject("Msxml2.XMLHTTP");
      //   } catch (e) {
      //     try {
      //       request = new ActiveXObject("Microsoft.XMLHTTP");
      //     } catch (e) {}
      //   }
      // }
      request.open("GET", img, true);
      request.send();
    });

    return (
      <Container>
        <div>
          {JSON.parse(episodeContent.content).map((img, i) => {
            return <img key={i} height="1000" width="800" src={img} />;
          })}
        </div>
      </Container>
    );
  }
  return <div>Coming soon</div>;
};

export default EpisodeDetail;
