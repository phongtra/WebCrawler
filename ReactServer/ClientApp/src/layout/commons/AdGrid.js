import React, { createRef } from "react";
import { Grid, Responsive, Segment, Sticky, Ref } from "semantic-ui-react";

const AdGrid = ({ children }) => {
  const adRef1 = createRef();
  const adRef2 = createRef();
  return (
    <Grid container columns={3}>
      <Ref innerRef={adRef1}>
        <Grid.Column width={1}>
          <Responsive minWidth={1024}>
            <Sticky context={adRef1}>
              <Segment></Segment>
            </Sticky>
          </Responsive>
        </Grid.Column>
      </Ref>

      <Grid.Column width={14}>
        <Responsive maxWidth={500}>
          <Segment></Segment>
        </Responsive>
        {children}
      </Grid.Column>
      <Ref innerRef={adRef2}>
        <Grid.Column width={1}>
          <Responsive minWidth={1024}>
            <Sticky context={adRef2}>
              <Segment></Segment>
            </Sticky>
          </Responsive>
        </Grid.Column>
      </Ref>
    </Grid>
  );
};

export default AdGrid;
